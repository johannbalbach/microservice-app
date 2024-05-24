using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Interfaces;
using Shared.DTO;
using User.Domain.Context;
using AutoMapper;
using User.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.JWT;
using System.IdentityModel.Tokens.Jwt;
using Shared.Models.Enums;
using Shared.Exceptions;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Security.Cryptography;
using Shared.DTO.Query;
using MassTransit;
using Shared.DTO.ServiceBusDTO;
using Response = Shared.Models.Response;

namespace User.BL.Services
{
    public class UserService: IUserService
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IRequestClient<GetManagerAccessBoolRequest> _getEnrollmentRequestClient;
        private readonly IRequestClient<GetDictionaryEntityExistBoolRequest> _getDictionaryRequestClient;
        public UserService(AuthDbContext context, IMapper mapper, ITokenService tokenService, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
            _getEnrollmentRequestClient = bus.CreateRequestClient<GetManagerAccessBoolRequest>();
            _getDictionaryRequestClient = bus.CreateRequestClient<GetDictionaryEntityExistBoolRequest>();
        }
        public async Task<ActionResult<Response>> ChangePassword(string password, string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new InvalidLoginException();
            if (Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password))) == user.Password)
                throw new BadRequestException("Вы ввели тот же самый пароль");

            user.Password = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
            await _context.SaveChangesAsync();

            return new Response { Status = "200", Message = "VSE OK" };
        }

        public async Task<ActionResult<Response>> LogoutUser(string token)
        {
            await _IsTokenValid(token);

            var temp = await _context.UserTokens.SingleOrDefaultAsync(h => h.AccessToken == token);

            if (temp == null)
                throw new InvalidLoginException();

            _context.UserTokens.Remove(temp);
            await _context.SaveChangesAsync();

            return new Response { Status = "200", Message = "VSE OK" };
        }

        public async Task<ActionResult<Response>> LoginUser(LoginCredentials login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == login.Email);

            if (user == null)
                throw new InvalidLoginException();

            if (login.Password != user.Password)
                throw new BadRequestException("Вы ввели неправильный пароль");

            var existingToken = await _context.UserTokens.SingleOrDefaultAsync(h => h.UserId == user.Id);

            if (existingToken != null)
            {
                _context.UserTokens.Remove(existingToken);
                await _context.SaveChangesAsync();  
            }

            var role = user.Roles.Contains(RoleEnum.Manager) ? RoleEnum.Manager : RoleEnum.Applicant;

            var accessToken = await _tokenService.GenerateAccessToken(login.Email, role);
            var refreshToken = await _tokenService.GenerateRefreshToken();

            await _context.UserTokens.AddAsync(new Token { 
                UserId = user.Id,
                LoginProvider = "site",
                Name = user.FullName,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(JWTConfiguration.RefreshLifeTime)
            });
            await _context.SaveChangesAsync();

            return new Response($"AccessToken: {accessToken}\nRefreshToken: {refreshToken}");
        }

        public async Task<ActionResult<UserProfileDTO>> UserProfileGet(string email)
        {
            var temp = await _context.Users.SingleOrDefaultAsync(h => h.Email == email);

            if (temp == null)
                throw new InvalidLoginException();

            return _mapper.Map<UserProfileDTO>(temp);
        }
        public async Task<ActionResult<ApplicantProfileDTO>> ApplicantProfileGet(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(h => h.Email == email);

            if (user == null)
                throw new InvalidLoginException();

            var applicant = await _context.Users.SingleOrDefaultAsync(a => a.Id == user.Id);

            var applicantDto = _mapper.Map<ApplicantProfileDTO>(applicant);
            applicantDto.FullName = user.FullName;
            applicantDto.Email = user.Email;
            applicantDto.Roles = user.Roles;

            return applicantDto;
        }

        public async Task<ActionResult<UserProfileDTO>> UserProfilePut(UserProfileEditDTO user, string email)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(h => h.Email == email);

            if (existingUser == null)
                throw new InvalidLoginException();

            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.NormalizedEmail = user.Email.Normalize();

            await _context.SaveChangesAsync();

            return _mapper.Map<UserProfileDTO>(existingUser);
        }

        public async Task<ActionResult<Response>> UserRegisterApplicantPost(ApplicantRegisterDTO body)
        {
            var user = _mapper.Map<UserE>(body);
            user.Id = new Guid();
            user.NormalizedEmail = body.Email.Normalize();
            user.Password = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(body.Password)));
            user.Roles.Add(RoleEnum.Applicant);

            var applicant = _mapper.Map<Applicant>(body);
            applicant.User = user;
            applicant.Id = user.Id;

            if (await _IsUserInDb(user))
            {
                throw new BadRequestException("user with that email is already in database");
            }

            await _context.Users.AddAsync(user);
            await _context.Applicants.AddAsync(applicant);
            await _context.SaveChangesAsync();

            var result = await LoginUser(new LoginCredentials
            {
                Email = user.Email,
                Password = user.Password,
            });

            return result;
        }

        public async Task<ActionResult<Response>> UserRegister(UserRegisterDTO body)
        {
            var user = _mapper.Map<UserE>(body);
            user.Id = Guid.NewGuid();
            user.FullName = body.FullName;
            user.NormalizedEmail = body.Email.Normalize();
            user.Password = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(body.Password)));

            if (await _IsUserInDb(user))
            {
                throw new BadRequestException("user with that email is already in database");
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var result = await LoginUser(new LoginCredentials
            {
                Email = user.Email,
                Password = user.Password,
            });

            return result;
        }
        public async Task<ActionResult<Response>> UpdateToken(string refresh_token)
        {
            var token = await _context.UserTokens.SingleOrDefaultAsync(h => h.RefreshToken == refresh_token);

            if (token == null)
                throw new InvalidLoginException("this refresh_token doesnt exist or already expired, please login first");
            if (token.RefreshTokenExpiration <= DateTime.Now)
            {
                _context.UserTokens.Remove(token);
                await _context.SaveChangesAsync();

                throw new InvalidLoginException("this refresh_token already expired, please login again");
            }
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == token.UserId);
            if (user == null)
                throw new NotFoundException("user who have this token not found");

            var accessToken = await _tokenService.GenerateAccessToken(user.Email, user.Roles.First());

            token.AccessToken = accessToken;
            _context.SaveChanges();

            return new Response("AccessToken: " + accessToken + "\nRefreshToken: " + token.RefreshToken);
        }
        public async Task<ActionResult<Response>> AddMainManager(Guid userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException("User with that guid not found");
            var manager = new Manager { User = user, FacultyId = null, Id = userId };

            if (!user.Roles.Contains(RoleEnum.MainManager))
            {
                user.Roles.Add(RoleEnum.MainManager);
                await _context.SaveChangesAsync();
            }
            if (await _IsManagerInDb(userId))
            {
                user.Roles.Remove(RoleEnum.Manager);

                var existManager = await _context.Managers.FirstOrDefaultAsync(x => x.Id == userId);
                _context.Managers.Remove(existManager);
                await _context.Managers.AddAsync(manager);
                await _context.SaveChangesAsync();

                return new Response { Status = "200", Message = "Manager successfully promoted to main manager" };
            }

            return new Response { Status = "200", Message = "Main manager added successfully" };
        }

        public async Task<ActionResult<Response>> AddManager(ManagerRegisterDTO body)
        {
            if (!(await IsFacultyExist(body.FacultyId)))
            {
                throw new NotFoundException("Faculty with that guid not found");
            }
            
            var user = _mapper.Map<UserE>(body);
            user.Id = Guid.NewGuid();
            user.NormalizedEmail = body.Email.Normalize();
            user.Password = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(body.Password)));
            user.Roles.Add(RoleEnum.Manager);

            var manager = new Manager{ 
                User = user,
                FacultyId = body.FacultyId,
                Id = user.Id
            };

            if (await _IsUserInDb(user))
            {
                throw new BadRequestException("User with that email is already in database");
            }

            await _context.Users.AddAsync(user);
            await _context.Managers.AddAsync(manager);
            await _context.SaveChangesAsync();

            return new Response { Status = "200", Message = "Manager added successfully" };
        }

        public async Task<ActionResult<ApplicantProfileDTO>> EditApplicantByIdManager(ApplicantProfileEditDTO body, Guid id, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                throw new NotFoundException("user with this token is not found");

            if (user.Roles.Contains(RoleEnum.Manager) && !( user.Roles.Contains(RoleEnum.MainManager) || user.Roles.Contains(RoleEnum.Admin)))
            {
                if (!await CheckAssign(id, user.Id))
                    throw new ForbiddenException("You dont have enough rights to edit this applicant");
            }

            var applicant = await _context.Applicants.SingleOrDefaultAsync(a => a.Id == id);

            if (applicant == null)
                throw new NotFoundException("Applicant not found");

            applicant.User.FullName = body.FullName;
            applicant.BirthDate = body.BirthDate;
            applicant.Gender = body.Gender;
            applicant.Citizenship = body.Citizenship;
            applicant.PhoneNumber = body.PhoneNumber;

            await _context.SaveChangesAsync();

            return _mapper.Map<ApplicantProfileDTO>(applicant);
        }

        public async Task<ActionResult<ApplicantProfileDTO>> GetApplicantByIdManager(Guid id)
        {
            var applicant = await _context.Applicants.SingleOrDefaultAsync(a => a.Id == id);

            if (applicant == null)
                throw new NotFoundException("Applicant not found");

            var applicantDto = _mapper.Map<ApplicantProfileDTO>(applicant);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == applicant.Id);

            applicantDto.FullName = user.FullName;
            applicantDto.Email = user.Email;
            applicantDto.Roles = user.Roles;

            return applicantDto;
        }

        public async Task<ActionResult<ApplicantWithPaginationInfo>> GetApplicantsListWithFiltering(ApplicantsFilterQuery query)
        {
            var applicantsQuery = _context.Applicants.AsQueryable();

            if (!string.IsNullOrEmpty(query.Search))
            {
                applicantsQuery = applicantsQuery.Where(a => a.User.FullName.Contains(query.Search));
            }

            if (query.BirthDateFrom.HasValue)
            {
                applicantsQuery = applicantsQuery.Where(a => a.BirthDate >= query.BirthDateFrom.Value);
            }

            if (query.BirthDateTo.HasValue)
            {
                applicantsQuery = applicantsQuery.Where(a => a.BirthDate <= query.BirthDateTo.Value);
            }

            if (query.Gender != null)
            {
                applicantsQuery = applicantsQuery.Where(a => a.Gender == query.Gender);
            }

            if (!string.IsNullOrEmpty(query.Citizenship))
            {
                applicantsQuery = applicantsQuery.Where(a => a.Citizenship.Contains(query.Citizenship));
            }

            if (!string.IsNullOrEmpty(query.PhoneNumber))
            {
                applicantsQuery = applicantsQuery.Where(a => a.PhoneNumber.Contains(query.PhoneNumber));
            }

            var applicantsList = await applicantsQuery.Skip((query.page - 1) * query.pageSize).Take(query.pageSize).ToListAsync();

            List<ApplicantWithIdDTO> applicantsDtos = new List<ApplicantWithIdDTO>();
            foreach(var appl in applicantsList)
            {
                var applDto = _mapper.Map<ApplicantWithIdDTO>(appl);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == appl.Id);

                applDto.FullName = user.FullName;
                applDto.Email = user.Email;
                applDto.Roles = user.Roles;
                applicantsDtos.Add(applDto);
            }

            return new ApplicantWithPaginationInfo
            {
                applicants = applicantsDtos,
                size = query.pageSize,
                pageCurrent = query.page,
                elementsCount = await applicantsQuery.CountAsync(),
            };
        }

        public async Task<ActionResult<ManagerWithPaginationInfo>> GetListOfManagersWithFiltering(ManagersFilterQuery query, string email)
        {
            var managersQuery = _context.Managers.AsQueryable();

            if (!string.IsNullOrEmpty(query.Type))
            {
                // Implement filtering by Type if necessary
            }

            if (query.FacultyId.HasValue)
            {
                //managersQuery = managersQuery.Where(m => m.FacultyId == query.FacultyId);
            }

            if (query.ApplicantId.HasValue)
            {
                // Implement filtering by ApplicantId if necessary
            }

            if (!string.IsNullOrEmpty(query.Search))
            {
                managersQuery = managersQuery.Where(m => m.User.FullName.Contains(query.Search));
            }

            // Implement sorting logic if necessary
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                // Implement sorting logic based on query.SortBy
            }

            var managersList = await managersQuery.ToListAsync();

            throw new NotImplementedException();

/*            return new ManagerWithPaginationInfo
            {
                Managers = _mapper.Map<List<ManagerProfileDTO>>(managersList),
                TotalCount = managersList.Count
            };*/
        }
        public async Task<ActionResult<Response>> CheckToken(string token)
        {
            await _IsTokenValid(token);

            return new Response { Status = "200", Message = "VSE OK" };
        }
        private async Task<bool> IsFacultyExist(Guid facultyId)
        {
            var response = await _getDictionaryRequestClient.GetResponse<DictionaryEntityExistBool>(new GetDictionaryEntityExistBoolRequest { EntityId = facultyId, entityType = Shared.Enums.DictionaryEntities.Faculty});

            return response.Message.Exist;
        }
        private async Task<bool> CheckAssign(Guid ApplicantId, Guid ManagerId)
        {
            var response = await _getEnrollmentRequestClient.GetResponse<ManagerAccess>(new GetManagerAccessBoolRequest { ApplicantId = ApplicantId, ManagerId = ManagerId });

            return response.Message.Access;
        }
        private async Task<bool> _IsUserInDb(UserE user)
        {
            var temp = await _context.Users.SingleOrDefaultAsync(h => h.Email == user.Email);

            return !(temp == null);
        }
        private async Task<bool> _IsManagerInDb(Guid managerId)
        {
            var temp = await _context.Managers.SingleOrDefaultAsync(h => h.Id == managerId);

            return !(temp == null);
        }
        private async Task _IsTokenValid(string token)
        {
            var alreadyExistsToken = await _context.UserTokens.FirstOrDefaultAsync(x => x.AccessToken == token);

            if (alreadyExistsToken == null)
            {
                throw new InvalidTokenException();
            }
        }
    }
}
