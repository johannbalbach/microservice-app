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

namespace User.BL.Services
{
    public class UserService: IUserService
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public UserService(AuthDbContext context, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        private async Task<bool> _IsUserInDb(UserE user)
        {
            if (user == null)
                return false;

            var temp = await _context.Users.SingleOrDefaultAsync(h => h.Email == user.Email);

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

            var temp = await _context.UserTokens.SingleOrDefaultAsync(h => h.UserId == user.Id);

            if (temp != null)
            {
                _context.UserTokens.Remove(temp);
                await _context.SaveChangesAsync();  
            }


            RoleEnum role = RoleEnum.Applicant;
            if (user.Roles.Contains(RoleEnum.Manager))
                role = RoleEnum.Manager;

            var AccessToken = await _tokenService.GenerateAccessToken(login.Email, role);
            var RefreshToken = await _tokenService.GenerateRefreshToken();

            await _context.UserTokens.AddAsync(new Token { 
                UserId = user.Id,
                LoginProvider = "site",
                Name = user.FullName,
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(JWTConfiguration.RefreshLifeTime)
            });
            await _context.SaveChangesAsync();

            return new Response("AccessToken: " + AccessToken + "\nRefreshToken: " + RefreshToken);
        }

        public async Task<ActionResult<UserProfileDTO>> UserProfileGet(string email)
        {
            var temp = await _context.Users.SingleOrDefaultAsync(h => h.Email == email);
            //applicant manager?

            if (temp == null)
                throw new InvalidLoginException();

            return _mapper.Map<UserProfileDTO>(temp);
        }

        public async Task<ActionResult<UserProfileDTO>> UserProfilePut(UserProfileEditDTO user, string email)
        {
            var temp = await _context.Users.SingleOrDefaultAsync(h => h.Email == email);

            if (temp == null)
                throw new InvalidLoginException();

            temp.FullName = user.FullName;
            temp.Email = user.Email;
            temp.NormalizedEmail = user.Email.Normalize();

            await _context.SaveChangesAsync();

            return _mapper.Map<UserProfileDTO>(temp);
        }

        public async Task<ActionResult<Response>> UserRegisterApplicantPost(ApplicantRegisterDTO body)
        {
            var applicant = _mapper.Map<Applicant>(body);

            var user = _mapper.Map<UserE>(body);
            user.Id = new Guid();
            user.NormalizedEmail = body.Email.Normalize();
            user.Password = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(body.Password)));
            user.Roles.Add(RoleEnum.Applicant);

            applicant.User = user;
            applicant.Id = user.Id;

            if (_IsUserInDb(user).Result)
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
            user.NormalizedEmail = body.Email.Normalize();
            user.Password = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(body.Password)));
            user.Roles.Add(RoleEnum.Manager);

            if (_IsUserInDb(user).Result)
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
    }
}
