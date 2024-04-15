using Newtonsoft.Json;
using Shared.Models.Enums;

namespace Shared.DTO
{
    public class ApplicantProfileEditDTO
    {
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public GenderEnum? Gender { get; set; }
        public string? Citizenship { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
