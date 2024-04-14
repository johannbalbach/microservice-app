using Shared.Models.Enums;

namespace User.Domain.Entities
{
    public class test
    {
        public string FullName { get; set; }
        public string NormalizedFullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum? Role { get; set; }
    }
}
