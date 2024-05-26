using Shared.Models;
using Shared.DTO;
using Shared.DTO.Query;
using Shared.Interfaces;
using Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


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

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<Response<string>>> ImportDictionary([FromQuery]ImportTypeEnum importType1, int page, int size)
        {
            return await _dictionaryService.ImportDictionary(new ImportDictionaryQuery { ImportType = importType1, Page = page, Size = size });

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<FacultyDTO>>> GetFaculties()
        {
            return await _dictionaryService.GetFaculties();
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<EducationLevelDTO>>> GetEducationLevels()
        {
            return await _dictionaryService.GetEducationLevels();
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<DocumentTypeDTO>>> GetEducationDocuments()
        {
            return await _dictionaryService.GetEducationDocuments();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProgramWithPaginationInfo>> GetListOfProgramsWithPaginationAndFiltering([FromQuery] ProgramsFilterQuery query)
        {
            return await _dictionaryService.GetListOfProgramsWithPaginationAndFiltering(query);
        }
    }
}
