using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Shared.Models;
using Shared.DTO;
using Shared.DTO.Query;
using Shared.Interfaces;

namespace Dictionary.Application.Controllers
{
    [ApiController]
    [Route("api/dictionary/[action]")]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService enrollmentService)
        {
            _dictionaryService = enrollmentService;
        }

        [HttpGet]
        [Route("/dictionary/checkStatus/{importId}")]
        public async Task<ActionResult<Response>> CheckDictionaryImportStatus([FromRoute][Required] Guid? importId)
        {
            return new Response();
        }
        [HttpPost]
        public async Task<ActionResult<Response>> ImportDictionary([FromQuery][Required()] object file)
        {
            return new Response();
        }

        [HttpGet]
        public async Task<ActionResult<List<FacultyDTO>>> GetFaculties()
        {
            return await _dictionaryService.GetFaculties();
        }

        [HttpGet]
        public async Task<ActionResult<List<ProgramDTO>>> GetListOfProgramsWithPaginationAndFiltering([FromQuery] ProgramsFilterQuery query)
        {
            return await _dictionaryService.GetListOfProgramsWithPaginationAndFiltering(query);
        }
    }
}
