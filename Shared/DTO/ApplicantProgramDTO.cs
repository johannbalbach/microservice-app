using Shared.Models.Enums;
using System.Text.Json.Serialization;

namespace Shared.DTO
{
    public class ApplicantProgramDTO
    {
        public Guid? Id { get; set; }
        public Guid? FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EducationLevelEnum? EducationLevel { get; set; }
        public string Language { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EducationFormEnum? EducationForm { get; set; }
        public int priority { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public StatusEnum Status { get; set; }
    }
}
