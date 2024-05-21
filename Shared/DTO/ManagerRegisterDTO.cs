using Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO
{
    public class ManagerRegisterDTO
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordValidation]
        public string Password { get; set; }
        public Guid FacultyId { get; set; }
    }
}
