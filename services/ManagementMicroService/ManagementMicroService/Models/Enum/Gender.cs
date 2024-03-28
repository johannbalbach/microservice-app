using System.Runtime.Serialization;

namespace ManagementMicroService.Models.Enum
{
    public enum GenderEnum
    {
        [EnumMember(Value = "male")]
        Male = 0,
        [EnumMember(Value = "female")]
        Female = 1
    }
}
