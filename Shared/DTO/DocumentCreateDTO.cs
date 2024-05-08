using Newtonsoft.Json;

namespace Shared.DTO
{
    public class DocumentCreateDTO
    {
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public Guid DocumentTypeId { get; set; }

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public int EducationLevelId { get; set; }
    }
}
