using Newtonsoft.Json;
using Shared.Enum;
using Shared.Models.Enums;

namespace Shared.DTO
{
    public class DocumentCreateDTO
    {
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DocumentTypeEnum? DocumentType { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EducationLevelEnum? EducationLevel { get; set; }
    }
}
