using System.ComponentModel.DataAnnotations;

namespace Document.Domain.Entities
{
    public class FileDocument
    {
        public Guid Id { get; set; }
        [Required]
        public string Path {  get; set; }
    }
}
