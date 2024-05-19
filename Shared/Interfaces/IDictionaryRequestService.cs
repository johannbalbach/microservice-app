namespace Shared.Interfaces
{
    public interface IDictionaryRequestService
    {
        Task<bool> IsUniversityProgramExist(Guid ProgramId);
    }
}
