using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Shared.Validators
{
    public class SeriesNumberPassportValidation: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            var regex = "^([0-9]{2}\\s{1}[0-9]{2})?([0-9]{6})?$";
            var seriesnumber = value.ToString();

            if (!Regex.IsMatch(seriesnumber, regex))
                return new ValidationResult("incorrect value for series and number");

            return ValidationResult.Success;
        }
    }
}
