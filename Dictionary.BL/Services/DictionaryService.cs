using AutoMapper;
using Dictionary.Domain.Context;
using Dictionary.Domain.Entities;
using Dictionary.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Query;
using Shared.DTO;
using Shared.Interfaces;
using Shared.Models;
using Shared.Enums;
using Shared.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Models.Enums;
using DocumentType = Dictionary.Domain.Entities.DocumentType;
using System.Reflection.Emit;
using System;

namespace Dictionary.BL.Services
{
    public class DictionaryService: IDictionaryService
    {
        private readonly IMapper _mapper;
        private readonly IProgramRepository<UniversityProgram> _programRepository;
        private readonly IRepository<Faculty> _facultyRepository;
        private readonly IRepository<EducationLevel> _educationLevelRepository;
        private readonly IRepository<DocumentType> _documentTypeRepository;
        private readonly IExternalSystemService _externalSystemService;

        public DictionaryService(IMapper mapper, IProgramRepository<UniversityProgram> programRepository, IRepository<Faculty> facultyRepository,
            IRepository<EducationLevel> educationLevelRepository,  IExternalSystemService externalSystemService, IRepository<DocumentType> documentTypeRepository)
        {
            _mapper = mapper;
            _programRepository = programRepository;
            _facultyRepository = facultyRepository;
            _educationLevelRepository = educationLevelRepository;
            _externalSystemService = externalSystemService;
            _documentTypeRepository = documentTypeRepository;
        }

        public async Task<ActionResult<Response<string>>> ImportDictionary(ImportDictionaryQuery query)
        {
            var response = query.ImportType switch
            {
                ImportTypeEnum.EducationLevel => await _externalSystemService.GetEducationLevelsAsync(),
                ImportTypeEnum.DocumentTypes => await _externalSystemService.GetDocumentTypesAsync(),
                ImportTypeEnum.Faculties => await _externalSystemService.GetFacultiesAsync(),
                ImportTypeEnum.Programs => await _externalSystemService.GetProgramsAsync(query.Page, query.Size),
                _ => throw new BadRequestException("SSSSSSSSSSSSSSSSUUUUUUROVO")
            }; 

            switch (query.ImportType)
            {
                case ImportTypeEnum.EducationLevel:
                    await ProcessImportResponseAsync<EducationLevel>(response, _educationLevelRepository, async level =>
                    {
                        level.Id++;
                        var existingLevel = await _educationLevelRepository.GetByIdIntAsync(level.Id);
                        if (existingLevel != null)
                        {
                            existingLevel.Name = level.Name;
                            await _educationLevelRepository.UpdateAsync(existingLevel);
                        }
                        else
                        {
                            await _educationLevelRepository.AddAsync(level);
                        }
                    });
                    return new Response<string>("Successful import Education level", response);
                case ImportTypeEnum.DocumentTypes:
                    await ProcessImportResponseAsync<DocumentType>(response, _documentTypeRepository, async doc =>
                    {
                        doc.EducationLevel.Id++;
                        doc.EducationLevel = await _educationLevelRepository.GetByIdIntAsync(doc.EducationLevel.Id);
                        doc.CreateTime = doc.CreateTime.ToUniversalTime();
                        List<EducationLevel> levels = new List<EducationLevel>();

                        if (doc.NextEducationLevels != null)
                        {
                            foreach (var level in doc.NextEducationLevels)
                            {
                                level.Id++;
                                var educationLevel = await _educationLevelRepository.GetByIdIntAsync(level.Id);
                                levels.Add(educationLevel);
                                
                                
                                educationLevel.DocumentTypes.Add(doc);
                            }
                        }
                        doc.NextEducationLevels = levels;
                        doc.EducationLevelId = doc.EducationLevel.Id;
                        if (doc.NextEducationLevels != null)
                        {
                            doc.NextEducationLevels.ForEach(l => doc.NextEducationLevelsId.Add(l.Id));
                        }

                        var existingDoc = await _documentTypeRepository.GetByIdAsync(doc.Id);
                        if (existingDoc != null)
                        {
                            existingDoc.Name = doc.Name;
                            existingDoc.EducationLevel = doc.EducationLevel;
                            existingDoc.NextEducationLevels = doc.NextEducationLevels;
                            existingDoc.CreateTime = doc.CreateTime;
                            await _documentTypeRepository.UpdateAsync(existingDoc);
                        }
                        else
                        {
                            await _documentTypeRepository.AddAsync(doc);
                        }
                    });
                    return new Response<string>("Successful import document types", response);
                case ImportTypeEnum.Faculties:
                    await ProcessImportResponseAsync<Faculty>(response, _facultyRepository, async faculty =>
                    {
                        var existingFaculty = await _facultyRepository.GetByIdAsync(faculty.Id);
                        faculty.createTime = faculty.createTime.ToUniversalTime();
                        if (existingFaculty != null)
                        {
                            existingFaculty.Name = faculty.Name;
                            existingFaculty.createTime = faculty.createTime;
                            await _facultyRepository.UpdateAsync(existingFaculty);
                        }
                        else
                        {
                            await _facultyRepository.AddAsync(faculty);
                        }
                    });
                    return new Response<string>("Successful import faculties", response);
                case ImportTypeEnum.Programs:
                    var jsonObject = JObject.Parse(response);

                    var programsArray = jsonObject["programs"];
                    var universityPrograms = JsonConvert.DeserializeObject<List<UniversityProgramDTO>>(programsArray.ToString());

                    foreach (var program in universityPrograms)
                    {
                        program.EducationLevel.Id++;
                        var existingProgram = await _programRepository.GetByIdAsync(program.Id);
                        program.CreateTime = program.CreateTime.ToUniversalTime();

                        var faculty = await _facultyRepository.GetByIdAsync(program.Faculty.Id);
                        var educationLevel = await _educationLevelRepository.GetByIdIntAsync(program.EducationLevel.Id);

                        if (existingProgram != null)
                        {
                            existingProgram.Name = program.Name;
                            existingProgram.Code = program.Code == null ? "" : program.Code;
                            existingProgram.Language = program.Language;
                            existingProgram.CreateTime = program.CreateTime;
                            existingProgram.EducationForm = EducationFormConverter.ConvertToEducationFormEnum(program.EducationForm);
                            existingProgram.FacultyId = program.Faculty.Id;
                            existingProgram.Faculty = faculty;
                            existingProgram.EducationLevel = educationLevel;
                            existingProgram.EducationLevelId = program.EducationLevel.Id;

                            await _programRepository.UpdateAsync(existingProgram);
                        }
                        else
                        {
                            var entity = new UniversityProgram
                            {
                                Name = program.Name,
                                Code = program.Code == null ? "" : program.Code,
                                CreateTime = program.CreateTime,
                                Language = program.Language,
                                EducationForm = EducationFormConverter.ConvertToEducationFormEnum(program.EducationForm),
                                FacultyId = program.Faculty.Id,
                                Faculty = faculty,
                                EducationLevel = educationLevel,
                                EducationLevelId = program.EducationLevel.Id,
                            };
                            await _programRepository.AddAsync(entity);
                        }
                    }

                    await _programRepository.SaveChangesAsync();
                    return new Response<string>("Successful import university programs", response);
                default:
                    throw new BadRequestException("Invalid import type");
            }

        }
        public async Task<ActionResult<List<FacultyDTO>>> GetFaculties()
        {
            var faculties = await _facultyRepository.GetAllAsync();
            var facultiesDTO = faculties.Select(e => _mapper.Map<FacultyDTO>(e)).ToList();
            return facultiesDTO;
        }

        public async Task<ActionResult<List<DocumentTypeDTO>>> GetEducationDocuments()
        {
            var documents = await _documentTypeRepository.GetAllAsync();
            var documentsDtos = new List<DocumentTypeDTO>();

            foreach (var document in documents)
            {
                var documentDto = _mapper.Map<DocumentTypeDTO>(document);

                documentDto.EducationLevel = document.EducationLevelId;

                if (document.NextEducationLevelsId != null)
                    document.NextEducationLevelsId.ForEach(l => documentDto.NextEducationLevels.Add(l));

                documentsDtos.Add(documentDto);
            }

            return documentsDtos;
        }
        public async Task<ActionResult<List<EducationLevelDTO>>> GetEducationLevels()
        {
            var levels = await _educationLevelRepository.GetAllAsync();
            var levelsDTO = levels.Select(e => _mapper.Map<EducationLevelDTO>(e)).ToList();
            return levelsDTO;
        }

        public async Task<ActionResult<ProgramWithPaginationInfo>> GetListOfProgramsWithPaginationAndFiltering(ProgramsFilterQuery query)
        {
            var programs = await _programRepository.GetProgramsWithPaginationAndFiltering(query);

            var programsDTO = new List<ProgramDTO>();

            foreach (var program in programs)
            {
                var programDTO = _mapper.Map<ProgramDTO>(program);
                var faculty = await _facultyRepository.GetByIdAsync(program.FacultyId);

                programDTO.FacultyName = faculty.Name;
                var educationLevel = (EducationLevel) await _educationLevelRepository.GetByIdIntAsync(program.EducationLevelId);
                programDTO.EducationLevel = educationLevel.Name;

                programsDTO.Add(programDTO);
            }

            var programsPagination = new ProgramWithPaginationInfo { Programs = programsDTO, size = query.Size, pageCurrent = query.Page, elementsCount = await _programRepository.ElementsCount()};

            return programsPagination;
        }

        private async Task ProcessImportResponseAsync<T>(string response, IRepository<T> repository, Func<T, Task> processAction) where T : class
        {
            var items = JsonConvert.DeserializeObject<List<T>>(response);
            foreach (var item in items)
            {
                await processAction(item);
            }
            await repository.SaveChangesAsync();
        }
    }
}
