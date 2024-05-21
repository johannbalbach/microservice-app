using Shared.DTO.ServiceBusDTO;

namespace Shared.Interfaces
{
    public interface IDictionaryRequestService
    {
        Task<bool> IsUniversityProgramExist(Guid ProgramId);
        Task<bool> IsDictionaryEntityExist(GetDictionaryEntityExistBoolRequest req);
    }
}
