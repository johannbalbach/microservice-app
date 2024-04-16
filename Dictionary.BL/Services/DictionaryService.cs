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

namespace Dictionary.BL.Services
{
    public class DictionaryService: IDictionaryService
    {
        private readonly ExternalSystemService _externalSystemService;//interface
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepository<UniversityProgram> _programRepository;
        private readonly IRepository<Faculty> _facultyRepository;

        public DictionaryService(AppDbContext context, IMapper mapper, IRepository<UniversityProgram> programRepository, IRepository<Faculty> facultyRepository)
        {
            _context = context;
            _mapper = mapper;
            _programRepository = programRepository;
            _facultyRepository = facultyRepository;
        }

        public DictionaryService(ExternalSystemService externalSystemService)
        {
            _externalSystemService = externalSystemService;
        }

        public async Task<string> GetEducationLevelsAsync()
        {
            return await _externalSystemService.GetEducationLevelsAsync();
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
