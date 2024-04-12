using ApplicantMicroService.Models.Enum;
using System.Text.Json.Serialization;

namespace Applicant.Application.DTO.Query
{
    public class GetProgramListQuery
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
        public long? FacultyId { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EducationLevelEnum? EducationLevel { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EducationFormEnum? EducationForm { get; set; }
        public string Language { get; set; }
        public string Search { get; set; }
    }
}
