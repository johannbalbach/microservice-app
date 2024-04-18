using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.DTO.Query;
using Shared.Interfaces;
using Shared.Models;

namespace Management.BL.Services
{
    public class ManagementService : IManagementService
    {
        public async Task<ActionResult<Response>> AddMainManager(Guid? userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Response>> AddManager(UserRegisterDTO body)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<ApplicantProfileDTO>> EditApplicantByIdManager(ApplicantProfileEditDTO body, Guid? id)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<ApplicantProfileDTO>> GetApplicantByIdManager(long? id)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<ApplicantWithPaginationInfo>> GetApplicantsListWithFiltering(ApplicantsFilterQuery query)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<ManagerWithPaginationInfo>> GetListOfManagersWithFiltering(ManagersFilterQuery query)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }
    }
}
