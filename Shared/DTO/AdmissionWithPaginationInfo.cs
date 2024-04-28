namespace Shared.Models.DTO
{
    public class AdmissionWithPaginationInfo
    {
        public List<AdmissionDTO> Admissions { get; set; }
        public int? size { get; set; }
        public int? elementsCount { get; set; }
        public int? pageCurrent { get; set; }
    }
}
