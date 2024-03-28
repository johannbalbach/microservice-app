using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;  
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using ApplicantMicroService.Models;
using ApplicantMicroService.Models.Query;

namespace ApplicantMicroService.Controllers
{ 
    [ApiController]
    public class ApplicantApiController : ControllerBase
    { 
        [HttpPost]
        [Route("/Applicant/addProgramToMyList/{id}")]
        public async Task<ActionResult<Response>> AddProgramToApplicantList([FromRoute][Required]Guid? id)
        { 

        }

        [HttpPut]
        [Route("/Applicant/changeProgramPriority/{id}")]

        public async Task<ActionResult<Response>> ChangeProgramPriority([FromBody] int priority, [FromRoute][Required]Guid? id)
        { 

        }

        [HttpGet]
        [Route("/Applicant/getMyPrograms")]
        public async Task<ActionResult<List<Program>>> GetApplicantPrograms()
        { 

        }

        [HttpGet]
        [Route("/Applicant/getListOfFaculties")]
        public async Task<ActionResult<List<Faculty>>> GetFaculties()
        { 

        }

        [HttpGet]
        [Route("/Applicant/getListOfPrograms")]
        public async Task<ActionResult<List<Program>>> GetListOfProgramsWithPaginationAndFiltering([FromQuery] ProgramsFilterQuery)
        { 

        }

        [HttpDelete]
        [Route("/Applicant/removeProgramFromMyList/{id}")]
        public async Task<ActionResult<Response>> RemoveProgramFromApplicantList([FromRoute][Required]Guid? id)
        { 

        }
    }
}
