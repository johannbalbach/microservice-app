using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.ServiceBusDTO
{
    public class GetManagerAccessBoolRequest
    {
        public Guid ApplicantId {  get; set; }
        public Guid ManagerId {  get; set; }
    }
}
