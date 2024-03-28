using System.Runtime.Serialization;
using EnrollmentMicroService.Models.Enum;
using Newtonsoft.Json;

namespace EnrollmentMicroService.Models
{
    public class Admission
    { 
        public Guid? Id { get; set; }
        public Guid? ApplicantId { get; set; }
        public long? ProgramId { get; set; }
        public int? Priority { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public StatusEnum? Status { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
