﻿using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Query;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IDictionaryService
    {
        Task<ActionResult<List<FacultyDTO>>> GetFaculties();
        Task<ActionResult<List<ProgramDTO>>> GetListOfProgramsWithPaginationAndFiltering(ProgramsFilterQuery query);
    }
}
