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

namespace Dictionary.BL.Services
{
    public class DictionaryService: IDictionaryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepository<UniversityProgram> _programRepository;
        private readonly IRepository<Faculty> _facultyRepository;
        private readonly IRepository<EducationLevel> _educationLevelRepository;
        private readonly IRepository<DocumentType> _documentTypeRepository;
        private readonly IExternalSystemService _externalSystemService;

        public DictionaryService(AppDbContext context, IMapper mapper, IRepository<UniversityProgram> programRepository, IRepository<Faculty> facultyRepository,
            IRepository<EducationLevel> educationLevelRepository,  IExternalSystemService externalSystemService, IRepository<DocumentType> documentTypeRepository)
        {
            _context = context;
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
            Console.WriteLine(response);

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

                        if (doc.NextEducationLevelId != null)
                        {
                            doc.NextEducationLevel = await _educationLevelRepository.GetByIdIntAsync(doc.NextEducationLevelId);
                        }

                        var existingDoc = await _documentTypeRepository.GetByIdAsync(doc.Id);
                        if (existingDoc != null)
                        {
                            existingDoc.Name = doc.Name;
                            // Обновляем также связанные объекты EducationLevel
                            existingDoc.EducationLevel = doc.EducationLevel;
                            existingDoc.NextEducationLevel = doc.NextEducationLevel;
                            existingDoc.EducationLevelId = doc.EducationLevel.Id;
                            existingDoc.NextEducationLevelId = doc.NextEducationLevel.Id;
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
                        if (existingFaculty != null)
                        {
                            existingFaculty.Name = faculty.Name;
                            existingFaculty.CreatedDate = faculty.CreatedDate;
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
                    var universityPrograms = JsonConvert.DeserializeObject<List<UniversityProgram>>(programsArray.ToString());

                    foreach (var program in universityPrograms)
                    {
                        program.Faculty = await _facultyRepository.GetByIdAsync(program.Faculty.Id);
                        program.EducationLevel = await _educationLevelRepository.GetByIdIntAsync(program.EducationLevel.Id);

                        var existingProgram = await _programRepository.GetByIdAsync(program.Id);
                        if (existingProgram != null)
                        {
                            Console.WriteLine("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOK");
                            existingProgram.Name = program.Name;
                            existingProgram.Code = program.Code;
                            existingProgram.Language = program.Language;
                            existingProgram.EducationForm = program.EducationForm;
                            existingProgram.FacultyId = program.Faculty.Id;
                            existingProgram.Faculty = program.Faculty;
                            existingProgram.EducationLevel = program.EducationLevel;
                            existingProgram.EducationLevelId = program.EducationLevel.Id;

                            await _programRepository.UpdateAsync(existingProgram);
                        }
                        else
                        {
                            await _programRepository.AddAsync(program);
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

        public async Task<ActionResult<List<ProgramDTO>>> GetListOfProgramsWithPaginationAndFiltering(ProgramsFilterQuery query)
        {
            // Здесь можно реализовать логику получения списка программ с пагинацией и фильтрацией
            throw new NotImplementedException();
        }
    }
}
