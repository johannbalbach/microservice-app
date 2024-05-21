using Dictionary.Domain.Entities;
using Dictionary.Domain.Repository;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Shared.DTO;
using Shared.DTO.ServiceBusDTO;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Interfaces;
using Shared.Models.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DocumentType = Dictionary.Domain.Entities.DocumentType;

namespace Dictionary.BL.Services
{
    public class DictionaryRequestService : IDictionaryRequestService
    {
        private readonly IProgramRepository<UniversityProgram> _programRepository;
        private readonly IRepository<Faculty> _facultyRepository;
        private readonly IRepository<EducationLevel> _educationLevelRepository;
        private readonly IRepository<DocumentType> _documentTypeRepository;

        public DictionaryRequestService(IProgramRepository<UniversityProgram> programRepository, IRepository<Faculty> facultyRepository, 
            IRepository<EducationLevel> educationLevelRepository, IRepository<DocumentType> documentTypeRepository)
        {
            _programRepository = programRepository;
            _facultyRepository = facultyRepository;
            _documentTypeRepository = documentTypeRepository;
            _educationLevelRepository = educationLevelRepository;
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
        public async Task<bool> IsDictionaryEntityExist(GetDictionaryEntityExistBoolRequest req)
        {
            switch (req.entityType)
            {
                case DictionaryEntities.DocumentType:
                    var document = _documentTypeRepository.GetByIdAsync((Guid)req.EntityId);

                    if (document == null)
                    {
                        return false;
                    }

                    return true;
                case DictionaryEntities.EducationLevel:
                    var educationLevel = _educationLevelRepository.GetByIdIntAsync((int)req.EntityIdInt);

                    if (educationLevel == null)
                    {
                        return false;
                    }

                    return true;
                case DictionaryEntities.Faculty:
                    var faculty = _programRepository.GetByIdAsync((Guid)req.EntityId);

                    if (faculty == null)
                    {
                        return false;
                    }

                    return true;
                default:
                    throw new BadRequestException("Invalid import type");
            }
        }
    }
}
