using System.Runtime.Serialization;

namespace ApplicantMicroService.Models.Enum
{
    public enum EducationFormEnum
    {
        [EnumMember(Value = "full_time")]
        FullTime = 0,
        [EnumMember(Value = "part_time")]
        PartTime = 1
    }
}
