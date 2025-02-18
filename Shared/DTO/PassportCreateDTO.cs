
using Shared.Validators;

namespace Shared.DTO
{
    public class PassportCreateDTO
    {
        [SeriesNumberPassportValidation]
        public string SeriesNumber { get; set; }
        public string IssuedBy { get; set; }
        public DateOnly IssuedDate { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        //^([0-9]{3}[-]{1}[0-9]{3})?$ код подразделения
    }
}
