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
using System.Linq;

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

        public async Task<bool> IsProgramsEducationLevelsSame(List<Guid> programsId)
        {
            List<UniversityProgram> programs = new List<UniversityProgram>();
            foreach (var programId in programsId)
            {
                var program  = await _programRepository.GetByIdAsync(programId);
                programs.Add(program);
            }
            var uniqueEducationLevels = programs.Select(p => p.EducationLevelId).Distinct().ToList();

            if (!checkUnique(uniqueEducationLevels, 3))
                return false;

            if (!checkUnique(uniqueEducationLevels, 2))
                return false;

            if (!checkUnique(uniqueEducationLevels, 5))
                return false;

            if (!checkPairs(uniqueEducationLevels, 4, 1))
                return false;

            if (!checkPairs(uniqueEducationLevels, 6, 7))
                return false;

            return true;
        }
        public async Task<bool> IsDocumentTypeSameToProgam(Guid ProgramId, List<Guid> DocumentTypeIds)
        {
            List<DocumentType> docs = new List<DocumentType>();
            DocumentTypeIds.ForEach(async d => docs.Add(await _documentTypeRepository.GetByIdAsync(d)));
            if (docs == null)
                throw new NotFoundException("DocumentType with that guid doesnt exist");

            var program = await _programRepository.GetByIdAsync(ProgramId);
            foreach(var doc in docs)
            {
                if (program.EducationLevelId == doc.EducationLevelId)
                    return true;
                if (doc.NextEducationLevelsId.Contains(doc.EducationLevelId))
                    return true;
            }
            return false;
        }
        public async Task<List<Guid>> MatchesDocumentTypesForPrograms(List<Guid> programsId)
        {
            List<UniversityProgram> programs = new List<UniversityProgram>();

            foreach (var id in programsId)
            {
                programs.Add(await _programRepository.GetByIdAsync(id));
            }
            if (programs == null)
                throw new NotFoundException("programs with that guid doesnt exist");
       
            List<int> levelsId = new List<int>();
            foreach(var program in programs)
            {
                if (!levelsId.Contains(program.EducationLevelId))
                    levelsId.Add(program.EducationLevelId);
            }

            List<Guid> response = new List<Guid>();
            var doc = await _documentTypeRepository.GetAllAsync();
            foreach (var level in levelsId)
            {
                var docs = doc.Where(d => d.NextEducationLevelsId.Contains(level)).ToList();
                foreach (var d in docs)
                {
                    if (!response.Contains(d.Id))
                        response.Add(d.Id);
                }
            }

            return response;
        }

        private bool checkPairs(List<int> arr, int firstV, int secondV)
        {
            if (arr.Contains(firstV))
            {
                if (arr.Count() > 2)
                    return false;
                if (!arr.Contains(secondV))
                    return false;
            }
            if (arr.Contains(secondV))
            {
                if (arr.Count() > 2)
                    return false;
                if (!arr.Contains(firstV))
                    return false;
            }
            return true;
        }
        private bool checkUnique(List<int> arr, int value)
        {
            if (arr.Contains(value))
            {
                if (arr.Count() > 1)
                    return false;
            }
            return true;
        }
    }
}
