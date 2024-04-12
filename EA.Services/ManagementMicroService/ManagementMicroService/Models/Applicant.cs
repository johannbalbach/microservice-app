using Management.Domain.Models.Enum;

namespace Management.Domain.Models
{
    public class Applicant
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }
    }
}
