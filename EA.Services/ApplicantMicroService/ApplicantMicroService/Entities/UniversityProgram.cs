using Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Applicant.Domain.Entities
{
    public class UniversityProgram
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedTime { get; set; }
        [Required]
        public Guid FacultyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Language { get; set; }
        public EducationLevelEnum? EducationLevel { get; set; }
        public EducationFormEnum? EducationForm { get; set; }
        public Faculty Faculty { get; set; } = null!;
    }
}
