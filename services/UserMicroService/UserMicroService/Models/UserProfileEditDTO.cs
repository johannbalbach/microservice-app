using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace UserMicroService.Models
{

    public class UserProfileEditDTO
    { 
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
