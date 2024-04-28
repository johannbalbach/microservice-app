using Enrollment.Domain.Models.Enum;
using Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Domain.Models.Query
{
    public class AdmissionsFilterQuery
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
        public Guid? ProgramId { get; set; }
        public Guid? FacultyId { get; set; }
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }
        public bool? UnassignedOnly { get; set; }
        public SortEnum SortBy { get; set; }
    }
}
