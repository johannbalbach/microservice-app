using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.DTO.Query;
using Shared.Interfaces;
using Shared.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;


namespace Management.Application.Controllers
{
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly IManagementService _managementService;

        public ManagementController(IManagementService managementService)
        {
            _managementService = managementService;
        }

        [HttpPost]
        [Route("/management/addMainManager/{userId}")]
        [SwaggerOperation("AddMainManager")]
        public async Task<ActionResult<Response>> AddMainManager([FromRoute][Required] Guid? userId)
        {
            return await _managementService.AddMainManager(userId);
        }

        [HttpPost]
        [Route("/management/addManager")]
        [SwaggerOperation("AddManager")]
        public async Task<ActionResult<Response>> AddManager([FromBody] UserRegisterDTO body)
        {
            return await _managementService.AddManager(body);
        }

        [HttpPut]
        [Route("/management/editApplicant/{id}")]
        public async Task<ActionResult<ApplicantProfileDTO>> EditApplicantByIdManager([FromBody] ApplicantProfileEditDTO body, [FromRoute][Required] Guid? id)
        {
            return await _managementService.EditApplicantByIdManager(body, id);
        }

        [HttpGet]
        [Route("/management/getApplicant/{id}")]
        public async Task<ActionResult<ApplicantProfileDTO>> GetApplicantByIdManager([FromRoute][Required] long? id)
        {
            return await _managementService.GetApplicantByIdManager(id);
        }

        [HttpGet]
        [Route("/management/getListOfApplicants")]
        public async Task<ActionResult<ApplicantWithPaginationInfo>> GetApplicantsListWithFiltering([FromQuery] ApplicantsFilterQuery query)
        {
            return await _managementService.GetApplicantsListWithFiltering(query);
        }

        [HttpGet]
        [Route("/management/getListOfManagers")]
        public async Task<ActionResult<ManagerWithPaginationInfo>> GetListOfManagersWithFiltering([FromQuery] ManagersFilterQuery query)
        {
            return await _managementService.GetListOfManagersWithFiltering(query);
        }
    }
}
