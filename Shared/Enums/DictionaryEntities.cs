using System.Runtime.Serialization;

namespace Shared.Enums
{
    public enum DictionaryEntities
    {
        [EnumMember(Value = "document_type")]
        DocumentType = 0,
        [EnumMember(Value = "education_level")]
        EducationLevel = 1,
        [EnumMember(Value = "faculty")]
        Faculty = 2
    }
}
