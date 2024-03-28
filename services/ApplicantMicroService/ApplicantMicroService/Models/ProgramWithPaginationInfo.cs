using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ApplicantMicroService.Models
{
    public class ProgramWithPaginationInfo
    {
        public List<Program> Programs { get; set; }
        public long? TotalElements { get; set; }
        public long? TotalPages { get; set; }
    }
}
