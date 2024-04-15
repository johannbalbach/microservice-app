﻿using Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Query
{
    public class ProgramsFilterQuery
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
        public long? FacultyId { get; set; }

        [EnumDataType(typeof(EducationLevelEnum))]
        public EducationLevelEnum educationLevel { get; set; }
        [EnumDataType(typeof(EducationFormEnum))]
        public EducationFormEnum educationForm { get; set; }
        public string Language { get; set; }
        public string Search { get; set; }
    }
}