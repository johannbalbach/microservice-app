using System.Runtime.Serialization;

namespace Shared.Models.Enums
{
    public enum RoleEnum
    {
        [EnumMember(Value = "applicant")]
        Applicant = 0,
        [EnumMember(Value = "manager")]
        Manager = 1,
        [EnumMember(Value = "main_manager")]
        MainManager = 2,
        [EnumMember(Value = "admin")]
        Admin = 3,
    }
}
