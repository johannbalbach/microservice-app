using Shared.Enum;
using System.Text.Json.Serialization;

namespace Document.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }
        public List<Guid?> ScansId { get; set; }
    }
}
