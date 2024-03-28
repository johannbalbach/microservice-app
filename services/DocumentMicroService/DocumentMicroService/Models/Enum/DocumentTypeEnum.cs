using System.Runtime.Serialization;

namespace DocumentMicroService.Models.Enum
{
    public enum DocumentTypeEnum
    {
        [EnumMember(Value = "education_document1")]
        Document1Enum = 0,
        [EnumMember(Value = "education_document2")]
        Document2Enum = 1
    }
}
