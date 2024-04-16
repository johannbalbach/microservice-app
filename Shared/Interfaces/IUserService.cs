using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Models;

namespace Shared.Interfaces
{
    public interface IUserService
    {
        Task<ActionResult<Response>> ChangePassword(string password, string email);

        Task<ActionResult<Response>> LogoutUser(string token);

        Task<ActionResult<Response>> LoginUser(LoginCredentials body);

        Task<ActionResult<UserProfileDTO>> UserProfileGet(string email);

        Task<ActionResult<UserProfileDTO>> UserProfilePut(UserProfileEditDTO body, string email);

        Task<ActionResult<Response>> UserRegisterApplicantPost(ApplicantRegisterDTO body);

        Task<ActionResult<Response>> UserRegister(UserRegisterDTO body);
    }
}
