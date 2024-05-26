using System.ComponentModel.DataAnnotations;

namespace Dictionary.Domain.Entities
{
    public class DocumentType
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; }
        public EducationLevel EducationLevel { get; set; } = null!;
        public int EducationLevelId { get; set; }
        public List<EducationLevel>? NextEducationLevels { get; set;} = null!;
        public List<int> NextEducationLevelsId { get; set; } = new List<int>();
    }
}
