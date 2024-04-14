using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.DTO.UserDTO;
using Shared.Interfaces;

namespace User.BL.Services
{
    public class UserService: IUserService
    {
        public async Task<ActionResult<Response>> ChangePassword(string password)
        {
            // Реализация изменения пароля
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<Response>> LogoutUser()
        {
            // Реализация выхода пользователя
            return new Response { Status = "200", Message = "VSE OK" };
        }

        public async Task<ActionResult<Response>> LoginUser(LoginCredentials body)
        {
            // Реализация входа пользователя
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<UserProfileDTO>> UserProfileGet()
        {
            // Реализация получения профиля пользователя
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<ApplicantProfileDTO>> UserProfilePut(UserProfileEditDTO body, string type)
        {
            // Реализация обновления профиля пользователя
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<Response>> UserRegisterApplicantPost(ApplicantRegisterDTO body)
        {
            // Реализация регистрации абитуриента
            throw new System.NotImplementedException();
        }

        public async Task<ActionResult<Response>> UserRegister(UserRegisterDTO body)
        {
            // Реализация регистрации пользователя
            throw new System.NotImplementedException();
        }
    }
}
