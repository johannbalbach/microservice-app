using System.Runtime.Serialization;

namespace Shared.Models.Enums
{
    public enum GenderEnum
    {
        [EnumMember(Value = "male")]
        MaleEnum = 0,
        [EnumMember(Value = "female")]
        FemaleEnum = 1
    }
}
