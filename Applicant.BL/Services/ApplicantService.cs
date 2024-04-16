using Applicant.Domain.Context;
using Applicant.Domain.Entities;
using Applicant.Domain.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTO;
using Shared.DTO.Query;
using Shared.Interfaces;
using Shared.Models;

namespace Applicant.BL.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepository<UniversityProgram> _programRepository;
        private readonly IRepository<Faculty> _facultyRepository;

        public ApplicantService(AppDbContext context, IMapper mapper, IRepository<UniversityProgram> programRepository, IRepository<Faculty> facultyRepository)
        {
            _context = context;
            _mapper = mapper;
            _programRepository = programRepository;
            _facultyRepository = facultyRepository;
        }

        public async Task<ActionResult<Response>> AddProgramToApplicantList(Guid? id)
        {
            // Здесь можно реализовать логику добавления программы к списку абитуриентов
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Response>> ChangeProgramPriority(int priority, Guid? id)
        {
            // Здесь можно реализовать логику изменения приоритета программы
            throw new NotImplementedException();
        }

        public async Task<ActionResult<List<ApplicantProgramDTO>>> GetApplicantPrograms()
        {
            // Здесь можно реализовать логику получения программ абитуриентов
            throw new NotImplementedException();
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

        public async Task<ActionResult<Response>> RemoveProgramFromApplicantList(Guid? id)
        {
            // Здесь можно реализовать логику удаления программы из списка абитуриентов
            throw new NotImplementedException();
        }
    }
}
