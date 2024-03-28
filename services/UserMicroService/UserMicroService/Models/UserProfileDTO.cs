using System.Runtime.Serialization;
using Newtonsoft.Json;
using UserMicroService.Models.Enum;

namespace UserMicroService.Models
{
    public class UserProfileDTO
    { 
        public string FullName { get; set; }
        public string Email { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public RoleEnum? Role { get; set; }
    }
}
