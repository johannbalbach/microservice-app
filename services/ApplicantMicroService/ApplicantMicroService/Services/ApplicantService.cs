using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApplicantMicroService.Models;
using ApplicantMicroService.Models.DTO;
using ApplicantMicroService.Models.Query;
using ApplicantMicroService.Services.Interfaces;


namespace ApplicantMicroService.Services
{
    public class ApplicantService: IApplicantService
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
