using System.Runtime.Serialization;

namespace Shared.Models.Enums
{
    public enum RoleEnum
    {
        [EnumMember(Value = "applicant")]
        ApplicantEnum = 0,
        [EnumMember(Value = "manager")]
        ManagerEnum = 1,
        [EnumMember(Value = "main_manager")]
        MainManagerEnum = 2
    }
}
