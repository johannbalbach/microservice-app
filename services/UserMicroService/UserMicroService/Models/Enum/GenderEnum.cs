using System.Runtime.Serialization;

namespace UserMicroService.Models.Enum
{
    public enum GenderEnum
    {
        [EnumMember(Value = "male")]
        MaleEnum = 0,
        [EnumMember(Value = "female")]
        FemaleEnum = 1
    }
}
