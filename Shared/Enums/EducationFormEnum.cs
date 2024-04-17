using System.Runtime.Serialization;

namespace Shared.Models.Enums
{
    public enum EducationFormEnum
    {
        [EnumMember(Value = "full_time")]
        Очная = 0,
        [EnumMember(Value = "part_time")]
        Заочная = 1
    }
}
