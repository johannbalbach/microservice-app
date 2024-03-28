using System.Runtime.Serialization;

namespace DocumentMicroService.Models.Enum
{
    public enum EducationLevelEnum
    {
        [EnumMember(Value = "education_level1")]
        Level1Enum = 0,
        [EnumMember(Value = "education_level2")]
        Level2Enum = 1
    }
}
