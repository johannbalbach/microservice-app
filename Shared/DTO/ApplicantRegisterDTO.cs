using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Shared.Models.Enums;
using Shared.Validators;

namespace Shared.DTO
{
    public class ApplicantRegisterDTO
    {
        [Required]
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public GenderEnum? Gender { get; set; }
        public string? Citizenship { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordValidation]
        public string Password { get; set; }
    }
}