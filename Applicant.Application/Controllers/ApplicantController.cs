using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Shared.Models;
using Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Shared.DTO;
using Shared.DTO.Query;

namespace Applicant.Application.Controllers
{
    [ApiController]
    [Route("api/applicant/[action]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicantProgramDTO>>> GetMyPrograms()
        {
            return await _applicantService.GetApplicantPrograms();
        }

        [HttpGet]
        public async Task<ActionResult<List<FacultyDTO>>> GetFaculties()
        {
            return await _applicantService.GetFaculties();
        }

        [HttpGet]
        public async Task<ActionResult<List<ProgramDTO>>> GetListOfProgramsWithPaginationAndFiltering([FromQuery] ProgramsFilterQuery query)
        {
            return await _applicantService.GetListOfProgramsWithPaginationAndFiltering(query);
        }

        [HttpPost]
        [Route("api/applicant/addProgramToMyList/{id}")]
        public async Task<ActionResult<Response>> AddProgramToApplicantList([FromRoute][Required] Guid? id)
        {
            return await _applicantService.AddProgramToApplicantList(id);
        }

        [HttpPut]
        [Route("api/applicant/changeProgramPriority/{id}")]
        public async Task<ActionResult<Response>> ChangeProgramPriority([FromBody] int priority, [FromRoute][Required] Guid? id)
        {
            return await _applicantService.ChangeProgramPriority(priority, id);
        }

        [HttpDelete]
        [Route("api/applicant/removeProgramFromMyList/{id}")]
        public async Task<ActionResult<Response>> RemoveProgramFromApplicantList([FromRoute][Required] Guid? id)
        {
            return await _applicantService.RemoveProgramFromApplicantList(id);
        }
    }
}
