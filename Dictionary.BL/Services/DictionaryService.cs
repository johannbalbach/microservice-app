using AutoMapper;
using Dictionary.Domain.Context;
using Dictionary.Domain.Entities;
using Dictionary.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Query;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Interfaces;
using Shared.Models;
using Shared.Enums;
using Shared.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Models.Enums;

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
                    var educationLevels = JsonConvert.DeserializeObject<List<EducationLevel>>(response);

                    foreach (var level in educationLevels)
                    {
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
                    }

                    await _educationLevelRepository.SaveChangesAsync();
                    return new Response<string>("Successful import Education level", response);
                case ImportTypeEnum.DocumentTypes:
                    var documentTypes = JsonConvert.DeserializeObject<List<DocumentType>>(response);

                    foreach (var doc in documentTypes)
                    {
                        doc.EducationLevel = await _educationLevelRepository.GetByIdIntAsync(doc.EducationLevel.Id);
                        doc.CreatedTime = doc.CreatedTime.ToUniversalTime();

                        if (doc.NextEducationLevelId != null)
                        {
                            doc.NextEducationLevel = await _educationLevelRepository.GetByIdIntAsync(doc.NextEducationLevelId);
                        }

                        var existingDoc = await _documentTypeRepository.GetByIdAsync(doc.Id);
                        if (existingDoc != null)
                        {
                            existingDoc.Name = doc.Name;
                            existingDoc.EducationLevel = doc.EducationLevel;
                            existingDoc.NextEducationLevel = doc.NextEducationLevel;
                            existingDoc.EducationLevelId = doc.EducationLevel.Id;
                            existingDoc.NextEducationLevelId = doc.NextEducationLevel.Id;
                            existingDoc.CreatedTime = doc.CreatedTime;
                            await _documentTypeRepository.UpdateAsync(existingDoc);
                        }
                        else
                        {
                            await _documentTypeRepository.AddAsync(doc);
                        }
                    }

                    await _documentTypeRepository.SaveChangesAsync();
                    return new Response<string>("Successful import document types", response);
                case ImportTypeEnum.Faculties:
                    var faculties = JsonConvert.DeserializeObject<List<Faculty>>(response);

                    foreach (var faculty in faculties)
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
                    }

                    await _facultyRepository.SaveChangesAsync();
                    return new Response<string>("Successful import faculties", response);
                case ImportTypeEnum.Programs:
                    var jsonObject = JObject.Parse(response);

                    var programsArray = jsonObject["programs"];
                    var universityPrograms = JsonConvert.DeserializeObject<List<UniversityProgramDTO>>(programsArray.ToString());

                    foreach (var program in universityPrograms)
                    {
                        var existingProgram = await _programRepository.GetByIdAsync(program.Id);
                        program.CreatedTime = program.CreatedTime.ToUniversalTime();

                        var faculty = await _facultyRepository.GetByIdAsync(program.Faculty.Id);
                        var educationLevel = await _educationLevelRepository.GetByIdIntAsync(program.EducationLevel.Id);

                        if (existingProgram != null)
                        {
                            existingProgram.Name = program.Name;
                            existingProgram.Code = program.Code == null ? "" : program.Code;
                            existingProgram.Language = program.Language;
                            existingProgram.CreatedTime = program.CreatedTime;
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
                                CreatedTime = program.CreatedTime,
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
    }
}
