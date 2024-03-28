using System.Runtime.Serialization;

namespace EnrollmentMicroService.Models.Enum
{
    public enum StatusEnum
    {
        [EnumMember(Value = "created")]
        CreatedEnum = 0,
        [EnumMember(Value = "under_review")]
        UnderReviewEnum = 1,
        [EnumMember(Value = "approved")]
        ApprovedEnum = 2,
        [EnumMember(Value = "rejected")]
        RejectedEnum = 3
    }
}
