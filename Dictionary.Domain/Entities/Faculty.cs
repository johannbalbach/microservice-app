using System.ComponentModel.DataAnnotations;

namespace Dictionary.Domain.Entities
{
    public class Faculty
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime createTime { get; set; }
    }
}
