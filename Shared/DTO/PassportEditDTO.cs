using Shared.Validators;

namespace Shared.DTO
{
    public class PassportEditDTO
    {
        [SeriesNumberPassportValidation]
        public string SeriesNumber { get; set; }
        public string IssuedBy { get; set; }
        public DateOnly IssuedDate { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
    }
}
