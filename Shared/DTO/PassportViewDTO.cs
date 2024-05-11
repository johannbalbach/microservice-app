namespace Shared.DTO
{
    public class PassportViewDTO
    {
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public string SeriesNumber { get; set; }
        public string IssuedBy { get; set; }
        public DateOnly IssuedDate { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public List<Guid> FilesId { get; set; } = new List<Guid>();
    }
}
