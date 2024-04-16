namespace Shared.Models.DTO
{
    public class AdmissionWithPaginationInfo
    {
        public List<AdmissionDTO> Admissions { get; set; }
        public long? TotalElements { get; set; }
        public long? TotalPages { get; set; }
    }
}
