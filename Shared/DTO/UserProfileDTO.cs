using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Shared.Models.Enums;

namespace Shared.DTO
{
    public class UserProfileDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<RoleEnum> Roles { get; set; }
    }
}
