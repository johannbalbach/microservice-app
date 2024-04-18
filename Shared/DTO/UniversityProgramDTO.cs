using Shared.Models.Enums;

namespace Shared.DTO
{
    public class UniversityProgramDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid FacultyId { get; set; }
        public int EducationLevelId { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public string Language { get; set; }
        public string? EducationForm { get; set; }
        public EducationLevelDTO EducationLevel { get; set; } = null!;
        public FacultyDTO Faculty { get; set; } = null!;
    }
}
