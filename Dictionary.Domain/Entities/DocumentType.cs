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
        public List<EducationLevel>? NextEducationLevels { get; set;} = null!;
    }
}
