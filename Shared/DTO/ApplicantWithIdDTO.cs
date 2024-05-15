using Shared.Models.Enums;

namespace Shared.DTO
{
    public class ApplicantWithIdDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
        public string? Citizenship { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<RoleEnum> Roles { get; set; } = new List<RoleEnum>();
    }
}
