namespace Shared.DTO.ServiceBusDTO
{
    public class EducationDocumentType
    {
        public List<Guid> DocumentTypeIds { get; set; } = new List<Guid>();
        public bool IsSuccess { get; set; } = false;
    }
}
