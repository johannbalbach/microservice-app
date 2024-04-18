using System.Runtime.Serialization;

namespace Shared.Models.Enums
{
    public enum EducationFormEnum
    {
        [EnumMember(Value = "full_time")]
        Full = 0,
        [EnumMember(Value = "part_time")]
        Part = 1,
        [EnumMember(Value = "mixed_time")]
        Mixed = 2
    }
    public class EducationFormConverter
    {
        public static EducationFormEnum ConvertToEducationFormEnum(string value)
        {
            switch (value)
            {
                case "Очная":
                    return EducationFormEnum.Full;
                case "Заочная":
                    return EducationFormEnum.Part;
                case "Очно-заочная":
                    return EducationFormEnum.Mixed;
                default:
                    throw new ArgumentException("Invalid education form value");
            }
        }
    }
}
