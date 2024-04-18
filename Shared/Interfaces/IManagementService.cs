using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Query;
using Shared.DTO;
using Shared.Models;

namespace Shared.Interfaces
{
    public interface IManagementService
    {
        Task<ActionResult<Response>> AddMainManager(Guid? userId);
        Task<ActionResult<Response>> AddManager(UserRegisterDTO body);
        Task<ActionResult<ApplicantProfileDTO>> EditApplicantByIdManager(ApplicantProfileEditDTO body, Guid? id);
        Task<ActionResult<ApplicantProfileDTO>> GetApplicantByIdManager(long? id);
        Task<ActionResult<ApplicantWithPaginationInfo>> GetApplicantsListWithFiltering(ApplicantsFilterQuery query);
        Task<ActionResult<ManagerWithPaginationInfo>> GetListOfManagersWithFiltering(ManagersFilterQuery query);
    }
}
