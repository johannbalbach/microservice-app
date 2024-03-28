using System.Runtime.Serialization;
using Newtonsoft.Json;
using UserMicroService.Models.Enum;

namespace UserMicroService.Models
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
