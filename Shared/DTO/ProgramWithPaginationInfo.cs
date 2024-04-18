using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Shared.DTO
{
    public class ProgramWithPaginationInfo
    {
        public List<ProgramDTO> Programs { get; set; }
        public int? size { get; set; }
        public int? elementsCount { get; set; }
        public int? pageCurrent { get; set; }
    }
}
