using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using EnrollmentMicroService.Models.Enum;
using Newtonsoft.Json;

namespace EnrollmentMicroService.Models.Entities
{
    public class Admission
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string ProgramId { get; set; }
        public int Priority { get; set; }
        public StatusEnum Status { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
