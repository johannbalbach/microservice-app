using System.ComponentModel.DataAnnotations;

namespace Dictionary.Domain.Entities
{
    public class DocumentType
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Name { get; set; }
        public int EducationLevelId { get; set; }
        public EducationLevel EducationLevel { get; set; } = null!;
        public int NextEducationLevelId { get; set; }
        public EducationLevel NextEducationLevel { get; set;} = null!;
    }
}
