using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.ServiceBusDTO
{
    public class DocumentRequestMessage
    {
        public string? ApplicantEmail { get; set; }
        public Guid? ApplicantId { get; set; }
        public Guid DocumentGuid { get; set; }
    }
}
