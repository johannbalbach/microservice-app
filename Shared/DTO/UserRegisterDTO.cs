using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Shared.Validators;

namespace Shared.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordValidation]
        public string Password { get; set; }
    }
}
