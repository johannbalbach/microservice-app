using Enrollment.Domain.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Exceptions;
using Shared.Interfaces;
using Shared.Models;
using Shared.Models.DTO;
using Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Application.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class EnrollmentController: ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPut]
        [Authorize(Policy = "MainManager")]
        [Route("enrollment/assignManager/{admissionId}")]
        public async Task<ActionResult<Response>> AssignManagerToAdmission([FromRoute][Required] Guid admissionId, Guid managerId)
        {
            return await _enrollmentService.AssignManagerToAdmission(admissionId, managerId);
        }

        [HttpPut]
        [Authorize(Policy = "MainManager")]
        [Route("enrollment/assignManagerToApplicant/{applicantId}")]
        public async Task<ActionResult<Response>> AssignManagerToApplicant([FromRoute][Required] Guid applicantId, Guid managerId)
        {
            return await _enrollmentService.AssignManagerToApplicant(applicantId, managerId);
        }

        [HttpPut]
        [Authorize(Policy = "Privileged")]
        [Route("enrollment/editAdmissionStatus/{id}")]
        public async Task<ActionResult<Response>> EditAdmissionStatus([FromBody] StatusEnum body, [FromRoute][Required] Guid id)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _enrollmentService.EditAdmissionStatus(body, id, userEmailClaim);
        }

        [HttpGet]
        [Authorize(Policy = "Privileged")]
        [Route("enrollment/getAdmissions/{id}")]
        public async Task<ActionResult<List<AdmissionDTO>>> GetApplicantAdmissions([FromRoute][Required] Guid id)
        {
            return await _enrollmentService.GetApplicantAdmissions(id);
        }

        [HttpGet]
        [Authorize(Policy = "Privileged")]
        [Route("enrollment/getListOfAdmissions")]
        public async Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting([FromQuery] AdmissionsFilterQuery query)
        {
            return await _enrollmentService.GetListOfAdmissionsWithPaginationFilteringAndSorting(query);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<AdmissionDTO>>> GetMyPrograms()
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _enrollmentService.GetMyAdmissions(userEmailClaim);
        }

        [Authorize]
        [HttpPost]
        [Route("enrollment/addProgramToMyList/{id}")]
        public async Task<ActionResult<Response>> AddProgramToApplicantList([FromRoute][Required] Guid id)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _enrollmentService.AddProgramToMyList(id, userEmailClaim);
        }

        [Authorize]
        [HttpPut]
        [Route("enrollment/changeProgramPriority/{id}")]
        public async Task<ActionResult<Response>> ChangeProgramPriority([FromBody] int priority, [FromRoute][Required] Guid id)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _enrollmentService.ChangeProgramPriority(priority, id, userEmailClaim);
        }

        [HttpDelete]
        [Authorize]
        [Route("enrollment/removeProgramFromMyList/{id}")]
        public async Task<ActionResult<Response>> RemoveProgramFromApplicantList([FromRoute][Required] Guid id)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _enrollmentService.RemoveAdmissionFromApplicantList(id, userEmailClaim);
        }
    }
}
