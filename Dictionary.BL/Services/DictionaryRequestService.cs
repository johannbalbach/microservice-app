using Dictionary.Domain.Entities;
using Dictionary.Domain.Repository;
using Shared.Interfaces;

namespace Dictionary.BL.Services
{
    public class DictionaryRequestService : IDictionaryRequestService
    {
        private readonly IProgramRepository<UniversityProgram> _programRepository;

        public DictionaryRequestService(IProgramRepository<UniversityProgram> programRepository)
        {
            _programRepository = programRepository;
        }

        public async Task<bool> IsUniversityProgramExist(Guid ProgramId)
        {
            var program = _programRepository.GetByIdAsync(ProgramId);

            if (program == null) 
            { 
                return false;
            }

            return true;
        }
    }
}
