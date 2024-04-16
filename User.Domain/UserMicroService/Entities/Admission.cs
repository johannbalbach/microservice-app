using Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities
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
