using Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Domain.Entities
{
    public class Admission
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid ProgramId { get; set; }
        public int Priority { get; set; }
        public StatusEnum Status { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
