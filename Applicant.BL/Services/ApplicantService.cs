using Applicant.Application.DTO;
using Applicant.Application.DTO.Query;
using Applicant.Application.Interfaces;
using Applicant.Domain.Context;
using Shared.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Applicant.BL.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly AppDbContext _context;
        public ApplicantService(AppDbContext context)
        {
            _context = context;
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
            return new List<FacultyDTO>();
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
