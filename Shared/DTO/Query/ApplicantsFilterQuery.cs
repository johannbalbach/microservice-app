using Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.DTO.Query
{
    public class ApplicantsFilterQuery
    {
        public string? Search { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public GenderEnum? Gender { get; set; }
        public string? Citizenship { get; set; }
        public string? PhoneNumber { get; set; }
        [Range(1, 1000)]
        public int pageSize { get; set; } = 10;
        [Range(1, 1000)]
        public int page { get; set; } = 1;
    }
}
