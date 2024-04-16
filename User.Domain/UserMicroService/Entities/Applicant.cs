using Shared.Models.Enums;

namespace User.Domain.Entities
{
    public class Applicant
    {
        public Guid Id { get; set; }
        public required UserE User {  get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
        public string? Citizenship { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Guid> Documents { get; set; } = new List<Guid>();
        public List<Guid> Enrollments { get; set; } = new List<Guid>();
    }
}
