using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace User.Domain.Models
{

    public class UserProfileEditDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
