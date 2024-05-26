
using Shared.DTO.ServiceBusDTO;

namespace Shared.Interfaces
{
    public interface IDocumentRequestService
    {
        Task<EducationDocumentType> GetApplicantEducationDocument(Guid ApplicantId);
    }
}
