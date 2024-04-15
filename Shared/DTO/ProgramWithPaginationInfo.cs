using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Shared.DTO
{
    public class ProgramWithPaginationInfo
    {
        public List<ProgramDTO> Programs { get; set; }
        public long? TotalElements { get; set; }
        public long? TotalPages { get; set; }
    }
}
