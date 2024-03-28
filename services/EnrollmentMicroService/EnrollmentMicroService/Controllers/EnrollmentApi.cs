using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using EnrollmentSystem.Common.Models;
using EnrollmentMicroService.Models;
using EnrollmentMicroService.Models.Query;
using EnrollmentMicroService.Models.Enum;

namespace EnrollmentMicroService.Controllers
{ 
    [ApiController]
    public class EnrollmentApiController : ControllerBase
    { 
        [HttpPut]
        [Route("/enrollment/assignManager/{admissionId}")]
        public async Task<ActionResult<Response>> AssignManagerToAdmission([FromRoute][Required]Guid? admissionId)
        { 
        }

        [HttpPut]
        [Route("/enrollment/assignManagerToApplicant/{applicantId}")]
        public async Task<ActionResult<Response>> AssignManagerToApplicant([FromRoute][Required]Guid? applicantId)
        { 
        }
        [HttpPut]
        [Route("/enrollment/editAdmissionStatus/{id}")]

        public async Task<ActionResult<Response>> EditAdmissionStatus([FromBody]StatusEnum body, [FromRoute][Required]Guid? id)
        { 
        }

        [HttpGet]
        [Route("/enrollment/getAdmission/{id}")]
        public async Task<ActionResult<Admission>> GetApplicantAdmission([FromRoute][Required]Guid? id)
        { 

        }

        [HttpGet]
        [Route("/enrollment/getListOfAdmissions")]

        public async Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting([FromQuery] AdmissionsFilterQuery query)
        { 

        }
    }
}
