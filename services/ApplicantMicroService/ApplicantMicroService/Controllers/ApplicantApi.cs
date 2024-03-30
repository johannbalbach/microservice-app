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
using ApplicantMicroService.Models.DTO;
using ApplicantMicroService.Services.Interfaces;

namespace ApplicantMicroService.Controllers
{
    [ApiController]
    public class ApplicantApiController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantApiController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpPost]
        [Route("/Applicant/addProgramToMyList/{id}")]
        public async Task<ActionResult<Response>> AddProgramToApplicantList([FromRoute][Required] Guid? id)
        {
            return await _applicantService.AddProgramToApplicantList(id);
        }

        [HttpPut]
        [Route("/Applicant/changeProgramPriority/{id}")]
        public async Task<ActionResult<Response>> ChangeProgramPriority([FromBody] int priority, [FromRoute][Required] Guid? id)
        {
            return await _applicantService.ChangeProgramPriority(priority, id);
        }

        [HttpGet]
        [Route("/Applicant/getMyPrograms")]
        public async Task<ActionResult<List<ApplicantProgramDTO>>> GetApplicantPrograms()
        {
            return await _applicantService.GetApplicantPrograms();
        }

        [HttpGet]
        [Route("/Applicant/getListOfFaculties")]
        public async Task<ActionResult<List<FacultyDTO>>> GetFaculties()
        {
            return await _applicantService.GetFaculties();
        }

        [HttpGet]
        [Route("/Applicant/getListOfPrograms")]
        public async Task<ActionResult<List<ProgramDTO>>> GetListOfProgramsWithPaginationAndFiltering([FromQuery] ProgramsFilterQuery query)
        {
            return await _applicantService.GetListOfProgramsWithPaginationAndFiltering(query);
        }

        [HttpDelete]
        [Route("/Applicant/removeProgramFromMyList/{id}")]
        public async Task<ActionResult<Response>> RemoveProgramFromApplicantList([FromRoute][Required] Guid? id)
        {
            return await _applicantService.RemoveProgramFromApplicantList(id);
        }
    }
}
