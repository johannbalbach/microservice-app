using Applicant.Domain.Context;
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
        public ApplicantService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<Response>> AddProgramToApplicantList(Guid? id)
        {
            return new Response();
        }

        public async Task<ActionResult<Response>> ChangeProgramPriority(int priority, Guid? id)
        {
            return new Response();
        }

        public async Task<ActionResult<List<ApplicantProgramDTO>>> GetApplicantPrograms()
        {
            return new List<ApplicantProgramDTO>();
        }

        public async Task<ActionResult<List<FacultyDTO>>> GetFaculties()
        {
            var faculties = await _context.Faculties.ToListAsync();
            var facultiesDTO = faculties.Select(e => _mapper.Map<FacultyDTO>(e)).ToList();

            return facultiesDTO;
        }

        public async Task<ActionResult<List<ProgramDTO>>> GetListOfProgramsWithPaginationAndFiltering(ProgramsFilterQuery query)
        {
            return new List<ProgramDTO>();
        }

        public async Task<ActionResult<Response>> RemoveProgramFromApplicantList(Guid? id)
        {
            return new Response();
        }
    }
}
