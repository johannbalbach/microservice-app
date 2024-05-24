using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.DTO.Query;
using Shared.Models;

namespace Shared.Interfaces
{
    public interface IUserService
    {
        Task<ActionResult<Response>> ChangePassword(string password, string email);
        Task<ActionResult<Response>> LogoutUser(string token);
        Task<ActionResult<Response>> LoginUser(LoginCredentials body);
        Task<ActionResult<UserProfileDTO>> UserProfileGet(string email);
        Task<ActionResult<ApplicantProfileDTO>> ApplicantProfileGet(string email);
        Task<ActionResult<UserProfileDTO>> UserProfilePut(UserProfileEditDTO body, string email);
        Task<ActionResult<Response>> UserRegisterApplicantPost(ApplicantRegisterDTO body);
        Task<ActionResult<Response>> UserRegister(UserRegisterDTO body);
        Task<ActionResult<Response>> UpdateToken(string refresh_token);
        Task<ActionResult<Response>> AddMainManager(Guid userId);
        Task<ActionResult<Response>> AddManager(ManagerRegisterDTO body);
        Task<ActionResult<ApplicantProfileDTO>> EditApplicantByIdManager(ApplicantProfileEditDTO body, Guid id, string email);
        Task<ActionResult<ApplicantProfileDTO>> GetApplicantByIdManager(Guid id);
        Task<ActionResult<ApplicantWithPaginationInfo>> GetApplicantsListWithFiltering(ApplicantsFilterQuery query);
        Task<ActionResult<ManagerWithPaginationInfo>> GetListOfManagersWithFiltering(ManagersFilterQuery query, string email);
        Task<ActionResult<Response>> CheckToken(string token);
    }
}
