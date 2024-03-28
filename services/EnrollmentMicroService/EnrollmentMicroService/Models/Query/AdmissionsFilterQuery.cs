using EnrollmentMicroService.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentMicroService.Models.Query
{
    public class AdmissionsFilterQuery
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
        public string Search { get; set; }
        public Guid? ProgramId { get; set; }
        public List<Guid?> FacultyIds { get; set; }
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }
        public bool? UnassignedOnly { get; set; }
        public string SortBy { get; set; }
    }
}
