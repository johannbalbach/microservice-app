using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums
{
    public enum DocumentType
    {
        [EnumMember(Value = "passport")]
        Passport = 0,
        [EnumMember(Value = "education_document")]
        EducationDoc = 1
    }
}
