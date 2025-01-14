using System.Runtime.Serialization;
using Newtonsoft.Json;
using Shared.Models.Enums;

namespace Shared.DTO
{
    public class ProgramDTO
    {
        public Guid? Id { get; set; }
        public Guid? FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public string? EducationLevel { get; set; }
        public string Language { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EducationFormEnum? EducationForm { get; set; }
    }
}
