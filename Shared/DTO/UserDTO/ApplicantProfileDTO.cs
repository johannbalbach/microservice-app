using Shared.Models.Enums;
using System.Text.Json.Serialization;

namespace Shared.DTO.UserDTO
{
    public class ApplicantProfileDTO
    {
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public GenderEnum? Gender { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public RoleEnum? Role { get; set; }
    }
}
