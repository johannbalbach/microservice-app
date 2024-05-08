using Shared.Models.Enums;
using System.Text.Json.Serialization;

namespace Shared.DTO.Query
{
    public class GetProgramListQuery
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
        public long? FacultyId { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public int? EducationLevelId { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EducationFormEnum? EducationForm { get; set; }
        public string Language { get; set; }
        public string Search { get; set; }
    }
}
