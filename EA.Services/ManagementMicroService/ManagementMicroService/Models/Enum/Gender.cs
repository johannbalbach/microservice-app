using System.Runtime.Serialization;

namespace Management.Domain.Models.Enum
{
    public enum GenderEnum
    {
        [EnumMember(Value = "male")]
        Male = 0,
        [EnumMember(Value = "female")]
        Female = 1
    }
}
