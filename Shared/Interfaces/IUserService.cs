using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Models;

namespace Shared.Interfaces
{
    public interface IUserService
    {
        Task<ActionResult<Response>> ChangePassword(string password);

        Task<ActionResult<Response>> LogoutUser();

        Task<ActionResult<Response>> LoginUser(LoginCredentials body);

        Task<ActionResult<UserProfileDTO>> UserProfileGet();

        Task<ActionResult<ApplicantProfileDTO>> UserProfilePut(UserProfileEditDTO body, string type);

        Task<ActionResult<Response>> UserRegisterApplicantPost(ApplicantRegisterDTO body);

        Task<ActionResult<Response>> UserRegister(UserRegisterDTO body);
    }
}
