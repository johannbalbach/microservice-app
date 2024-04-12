using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Management.Domain.Models;
using Management.Domain.Models.Query;

namespace Management.Domain.Controllers
{
    [ApiController]
    public class ManagementApiController : ControllerBase
    {
        [HttpPost]
        [Route("/management/addMainManager/{userId}")]
        [SwaggerOperation("AddMainManager")]
        public async Task<ActionResult<Response>> AddMainManager([FromRoute][Required] Guid? userId)
        {

        }
        [HttpPost]
        [Route("/management/addManager")]
        [SwaggerOperation("AddManager")]
        public async Task<ActionResult<Response>> AddManager([FromBody] Manager body)
        {

        }

        [HttpPut]
        [Route("/management/editApplicant/{id}")]
        public async Task<ActionResult<Applicant>> EditApplicantByIdManager([FromBody] Applicant body, [FromRoute][Required] Guid? id)
        {

        }

        [HttpGet]
        [Route("/management/getApplicant/{id}")]
        public async Task<ActionResult<Applicant>> GetApplicantByIdManager([FromRoute][Required] long? id)
        {

        }

        [HttpGet]
        [Route("/management/getListOfApplicants")]
        public async Task<ActionResult<List<Applicant>>> GetApplicantsListWithFiltering([FromQuery] ApplicantsFilterQuery)
        {

        }

        [HttpGet]
        [Route("/management/getListOfManagers")]
        public async Task<ActionResult<List<Manager>>> GetListOfManagersWithFiltering([FromQuery] ManagersFilterQuery)
        {

        }
    }
}
