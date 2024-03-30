using System.Runtime.Serialization;

namespace EnrollmentMicroService.Models.Enum
{
    public enum StatusEnum
    {
        [EnumMember(Value = "created")]
        Created = 0,
        [EnumMember(Value = "under_review")]
        UnderReview = 1,
        [EnumMember(Value = "approved")]
        Approved = 2,
        [EnumMember(Value = "rejected")]
        Rejected = 3,
        [EnumMember(Value = "closed")]
        Closed = 4
    }
}
