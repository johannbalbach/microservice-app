using System.Runtime.Serialization;
using DocumentMicroService.Models.Enum;
using Newtonsoft.Json;

namespace DocumentMicroService.Models
{
    public class DocumentCreateDTO
    { 
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DocumentTypeEnum? DocumentType { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EducationLevelEnum? EducationLevel { get; set; }
    }
}
