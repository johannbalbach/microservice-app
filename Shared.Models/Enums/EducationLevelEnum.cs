using System.Runtime.Serialization;

namespace Shared.Models.Enums
{
    public enum EducationLevelEnum
    {
        [EnumMember(Value = "bachelor")]
        Bachelor = 0,
        [EnumMember(Value = "master")]
        Master = 1,
        [EnumMember(Value = "postgraduate")]
        Postgraduate = 2
    }
}
