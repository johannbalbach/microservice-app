using System.Runtime.Serialization;

namespace Shared.Models.Enums
{
    public enum EducationLevelEnum123
    {
        [EnumMember(Value = "bachelor")]
        Bachelor = 0,
        [EnumMember(Value = "master")]
        Master = 1,
        [EnumMember(Value = "postgraduate")]
        Postgraduate = 2,
        [EnumMember(Value = "specialty")]
        Specialty = 3,
        [EnumMember(Value = "basic_general_education")]
        BasicGeneral = 4,
        [EnumMember(Value = "secondary_general_education")]
        SecondaryGeneral = 5,
        [EnumMember(Value = "secondary_vocational_education")]
        SecondaryVocational = 6
    }
}
