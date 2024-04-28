using Enrollment.Domain.Models.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Interfaces;
using Shared.Models;
using Shared.Models.DTO;
using Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Application.Controllers
{
    [ApiController]
    [Route("api/enrollment/[action]")]
    public class EnrollmentController: ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPut]
        [Route("/enrollment/assignManager/{admissionId}")]
        public async Task<ActionResult<Response>> AssignManagerToAdmission([FromRoute][Required] Guid admissionId, Guid managerId)
        {
            return await _enrollmentService.AssignManagerToAdmission(admissionId, managerId);
        }

        [HttpPut]
        [Route("/enrollment/assignManagerToApplicant/{applicantId}")]
        public async Task<ActionResult<Response>> AssignManagerToApplicant([FromRoute][Required] Guid applicantId, Guid managerId)
        {
            return await _enrollmentService.AssignManagerToApplicant(applicantId, managerId);
        }

        [HttpPut]
        [Route("/enrollment/editAdmissionStatus/{id}")]
        public async Task<ActionResult<Response>> EditAdmissionStatus([FromBody] StatusEnum body, [FromRoute][Required] Guid id)
        {
            return await _enrollmentService.EditAdmissionStatus(body, id);
        }

        [HttpGet]
        [Route("/enrollment/getAdmissions/{id}")]
        public async Task<ActionResult<List<AdmissionDTO>>> GetApplicantAdmissions([FromRoute][Required] Guid id)
        {
            return await _enrollmentService.GetApplicantAdmissions(id);
        }

        [HttpGet]
        [Route("/enrollment/getListOfAdmissions")]
        public async Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting([FromQuery] AdmissionsFilterQuery query)
        {
            return await _enrollmentService.GetListOfAdmissionsWithPaginationFilteringAndSorting(query);
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicantProgramDTO>>> GetMyPrograms()
        {
            return await _enrollmentService.GetApplicantPrograms();
        }
        [HttpPost]
        [Route("api/applicant/addProgramToMyList/{id}")]
        public async Task<ActionResult<Response>> AddProgramToApplicantList([FromRoute][Required] Guid id)
        {
            return await _enrollmentService.AddProgramToApplicantList(id);
        }

        [HttpPut]
        [Route("api/applicant/changeProgramPriority/{id}")]
        public async Task<ActionResult<Response>> ChangeProgramPriority([FromBody] int priority, [FromRoute][Required] Guid id)
        {
            return await _enrollmentService.ChangeProgramPriority(priority, id);
        }

        [HttpDelete]
        [Route("api/applicant/removeProgramFromMyList/{id}")]
        public async Task<ActionResult<Response>> RemoveProgramFromApplicantList([FromRoute][Required] Guid id)
        {
            return await _enrollmentService.RemoveProgramFromApplicantList(id);
        }
    }
}
