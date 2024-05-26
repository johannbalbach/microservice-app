using Shared.DTO.ServiceBusDTO;

namespace Shared.Interfaces
{
    public interface IDictionaryRequestService
    {
        Task<bool> IsUniversityProgramExist(Guid ProgramId);
        Task<bool> IsDictionaryEntityExist(GetDictionaryEntityExistBoolRequest req);
        Task<bool> IsProgramsEducationLevelsSame(List<Guid> programsId);
        Task<bool> IsDocumentTypeSameToProgam(Guid ProgramId, List<Guid> DocumentTypeId);
        Task<List<Guid>> MatchesDocumentTypesForPrograms(List<Guid> ProgramId);
    }
}
