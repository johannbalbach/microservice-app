
namespace Shared.DTO.ServiceBusDTO
{
    public class ApplicantProgramsDocuments
    {
        public List<Guid> ProgramsDocumentsIds { get; set; } = new List<Guid>();
        public bool isEmpty { get; set; }
    }
}
