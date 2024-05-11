namespace Document.Domain.Entities
{
    public class Passport: Document
    {
        public Passport()
        {
            DocumentType = Shared.Enums.DocumentType.Passport;
        }
        public Passport(string seriesnumber, string issuedby, string placeofbirth, DateOnly issueddate, DateOnly birthdate)
        {
            DocumentType = Shared.Enums.DocumentType.Passport;
            SeriesNumber = seriesnumber;
            IssuedBy = issuedby;
            IssuedDate = issueddate;
            BirthDate = birthdate;
            PlaceOfBirth = placeofbirth;
        }
        public string SeriesNumber { get; set; }
        public string IssuedBy { get; set; }
        public DateOnly IssuedDate {  get; set; }
        public DateOnly BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
    }
}
