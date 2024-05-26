using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Query;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Interfaces
{
    public interface IDictionaryService
    {
        Task<ActionResult<List<FacultyDTO>>> GetFaculties();
        Task<ActionResult<List<DocumentTypeDTO>>> GetEducationDocuments();
        Task<ActionResult<List<EducationLevelDTO>>> GetEducationLevels();
        Task<ActionResult<ProgramWithPaginationInfo>> GetListOfProgramsWithPaginationAndFiltering(ProgramsFilterQuery query);
        Task<ActionResult<Response<string>>> ImportDictionary(ImportDictionaryQuery query);
    }
}
