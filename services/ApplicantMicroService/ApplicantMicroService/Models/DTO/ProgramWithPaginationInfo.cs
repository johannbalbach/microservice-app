using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ApplicantMicroService.Models.DTO
{
    public class ProgramWithPaginationInfo
    {
        public List<ProgramDTO> Programs { get; set; }
        public long? TotalElements { get; set; }
        public long? TotalPages { get; set; }
    }
}
