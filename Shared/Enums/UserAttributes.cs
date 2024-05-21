using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums
{
    public enum UserAttributes
    {
        [EnumMember(Value = "enrollment")]
        Enrollment = 0,
        [EnumMember(Value = "document")]
        Document = 1
    }
}
