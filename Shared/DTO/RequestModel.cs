using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class RequestModel
    {
        public Guid ReceiverId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public PossibleActionEnum Action { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }
}
