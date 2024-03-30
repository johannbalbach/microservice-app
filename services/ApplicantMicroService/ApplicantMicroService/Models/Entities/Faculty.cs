using System.ComponentModel.DataAnnotations;

namespace ApplicantMicroService.Models.Entities
{
    public class Faculty
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
