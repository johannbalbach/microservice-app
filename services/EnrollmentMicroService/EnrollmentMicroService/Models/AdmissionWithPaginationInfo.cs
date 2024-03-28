using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace EnrollmentMicroService.Models
{
    public class AdmissionWithPaginationInfo
    { 
        public List<Admission> Admissions { get; set; }
        public long? TotalElements { get; set; }
        public long? TotalPages { get; set; }
    }
}
