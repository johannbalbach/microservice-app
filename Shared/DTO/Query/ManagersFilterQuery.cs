using Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.Query
{
    public class ManagersFilterQuery
    {
        public RoleEnum? Type { get; set; }
        public Guid? FacultyId { get; set; }
        public string? Search { get; set; }
        [Range(1, 1000)]
        public int pageSize { get; set; } = 10;
        [Range(1, 1000)]
        public int page { get; set; } = 1;
    }
}
