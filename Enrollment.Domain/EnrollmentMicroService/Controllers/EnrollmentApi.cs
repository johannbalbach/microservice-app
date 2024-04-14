using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using EnrollmentMicroService.Models.Entities;
using Enrollment.Domain.Services.Interfaces;
using Enrollment.Domain.Models;
using Enrollment.Domain.Models.DTO;
using Enrollment.Domain.Models.Query;
using Enrollment.Domain.Models.Enum;

namespace Enrollment.Domain.Controllers
{
    [ApiController]
    public class EnrollmentApiController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentApiController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPut]
        [Route("/enrollment/assignManager/{admissionId}")]
        public async Task<ActionResult<Response>> AssignManagerToAdmission([FromRoute][Required] Guid? admissionId)
        {
            return await _enrollmentService.AssignManagerToAdmission(admissionId);
        }

        [HttpPut]
        [Route("/enrollment/assignManagerToApplicant/{applicantId}")]
        public async Task<ActionResult<Response>> AssignManagerToApplicant([FromRoute][Required] Guid? applicantId)
        {
            return await _enrollmentService.AssignManagerToApplicant(applicantId);
        }

        [HttpPut]
        [Route("/enrollment/editAdmissionStatus/{id}")]
        public async Task<ActionResult<Response>> EditAdmissionStatus([FromBody] StatusEnum body, [FromRoute][Required] Guid? id)
        {
            return await _enrollmentService.EditAdmissionStatus(body, id);
        }

        [HttpGet]
        [Route("/enrollment/getAdmissions/{id}")]
        public async Task<ActionResult<List<AdmissionDTO>>> GetApplicantAdmissions([FromRoute][Required] Guid? id)
        {
            return await _enrollmentService.GetApplicantAdmissions(id);
        }

        [HttpGet]
        [Route("/enrollment/getListOfAdmissions")]
        public async Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting([FromQuery] AdmissionsFilterQuery query)
        {
            return await _enrollmentService.GetListOfAdmissionsWithPaginationFilteringAndSorting(query);
        }
    }
}
