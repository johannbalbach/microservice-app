namespace Shared.DTO.ServiceBusDTO
{
    public class GetDocTypeEducationLevelBelongs
    {
        public List<Guid> DocumentTypeIds { get; set; } = new List<Guid>();
        public Guid ProgramId { get; set; }
    }
}
