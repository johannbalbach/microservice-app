using System.Runtime.Serialization;
using Newtonsoft.Json;
using User.Domain.Models.Enum;

namespace User.Domain.Models
{
    public class UserProfileDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public RoleEnum? Role { get; set; }
    }
}
