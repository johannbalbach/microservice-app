using Newtonsoft.Json;
using Shared.Models.Enums;
using Shared.Validators;

namespace Shared.DTO
{
    public class ApplicantProfileEditDTO
    {
        public string FullName { get; set; }
        [BirthDateValidator]
        public DateTime BirthDate { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public GenderEnum? Gender { get; set; }
        public string? Citizenship { get; set; }
        [PhoneNumberValidator]
        public string PhoneNumber { get; set; }
    }
}
