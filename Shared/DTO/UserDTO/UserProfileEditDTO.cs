using System.Runtime.Serialization;
using Newtonsoft.Json;


namespace Shared.DTO.UserDTO
{
    public class UserProfileEditDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
