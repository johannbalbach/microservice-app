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

namespace User.BL.Services
{
    public class UserService: IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public UserService(AppDbContext context, IMapper mapper, ITokenService tokenService)
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
/*            var alreadyExistsToken = await _context.UserTokens.FirstOrDefaultAsync(x => x.AccessToken == token);

            if (alreadyExistsToken == null)
            {
                throw new InvalidTokenException();
            }*/
        }


        public async Task<ActionResult<Response>> ChangePassword(string password)
        {
            // Реализация изменения пароля
            return new Response { Status = "200", Message = "VSE OK" };
        }

        public async Task<ActionResult<Response>> LogoutUser()
        {
            // Реализация выхода пользователя
            return new Response { Status = "200", Message = "VSE OK" };
        }

        public async Task<ActionResult<Response>> LoginUser(LoginCredentials login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == login.Email);

            if (user == null)
                throw new InvalidLoginException();

            if (user.Password != login.Password)
                throw new BadRequestException("wrong password, pls try again");

            var temp = await _context.Tokens.SingleOrDefaultAsync(h => h.UserId == user.Id);

            if (temp != null)
            {
                _context.Tokens.Remove(temp);
                await _context.SaveChangesAsync();
            }

            var AccessToken = await _tokenService.GenerateAccessToken(login.Email, RoleEnum.Applicant);
            var RefreshToken = await _tokenService.GenerateRefreshToken();

            await _context.Tokens.AddAsync(new Token { AccessToken = AccessToken, RefreshToken = RefreshToken, UserId = user.Id, User=user });
            await _context.SaveChangesAsync();

            return new Response("AccessToken: " + AccessToken + "/nRefreshToken: " + RefreshToken);
        }

        public async Task<ActionResult<UserProfileDTO>> UserProfileGet()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "string");
            var userDTO = _mapper.Map<UserProfileDTO>(user);

            return userDTO;
        }

        public async Task<ActionResult<ApplicantProfileDTO>> UserProfilePut(UserProfileEditDTO body, string type)
        {
            // Реализация обновления профиля пользователя
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<Response>> UserRegisterApplicantPost(ApplicantRegisterDTO body)
        {
            // Реализация регистрации абитуриента
            return new Response { Status = "200", Message = "VSE OK" };
        }

        public async Task<ActionResult<Response>> UserRegister(UserRegisterDTO body)
        {
            var user = _mapper.Map<UserE>(body);
            user.NormalizedEmail = body.Email.Normalize();
            user.NormalizedUserName = body.FullName.Normalize();

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
