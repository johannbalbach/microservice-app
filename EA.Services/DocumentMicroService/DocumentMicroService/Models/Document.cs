using System.Runtime.Serialization;
using Document.Domain.Models.Enum;
using Newtonsoft.Json;

namespace Document.Domain.Models
{
    public class Document
    {
        public Guid? Id { get; set; }
        public Guid? ApplicantId { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DocumentTypeEnum? DocumentType { get; set; }
        public List<Guid?> ScansId { get; set; }
    }
}
