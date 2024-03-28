using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace UserMicroService.Models
{
    public class UserRegisterDTO
    { 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
