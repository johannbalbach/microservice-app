using System.Runtime.Serialization;
using Document.Domain.Models.Enum;
using Newtonsoft.Json;

namespace Document.Domain.Models
{
    public class DocumentCreateDTO
    {
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DocumentTypeEnum? DocumentType { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EducationLevelEnum? EducationLevel { get; set; }
    }
}
