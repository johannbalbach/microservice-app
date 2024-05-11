namespace Shared.DTO
{
    public class EducationDocumentViewDTO
    {
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public string Name { get; set; }
        public Guid DocumentTypeId { get; set; }
        public List<Guid> FilesId { get; set; } = new List<Guid>();
    }
}
