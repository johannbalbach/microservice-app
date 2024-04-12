using System.Runtime.Serialization;
using Newtonsoft.Json;
using User.Domain.Models.Enum;

namespace User.Domain.Models
{
    public class ApplicantRegisterDTO
    {
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public GenderEnum? Gender { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public string Password { get; set; }
    }
}
