using System.Runtime.Serialization;
using DocumentMicroService.Models.Enum;
using Newtonsoft.Json;

namespace DocumentMicroService.Models
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
