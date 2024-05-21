using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.ServiceBusDTO
{
    public class AddAttributeToUserRequest
    {
        public Guid UserId { get; set; }
        public Guid AttributeId { get; set; }
        public UserAttributes UserAttributes { get; set; }
    }
}
