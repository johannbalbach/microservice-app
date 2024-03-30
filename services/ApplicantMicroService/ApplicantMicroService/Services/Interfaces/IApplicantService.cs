using ApplicantMicroService.Models.DTO;
using ApplicantMicroService.Models.Query;
using ApplicantMicroService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApplicantMicroService.Services.Interfaces
{
    public interface IApplicantService
    {
        Task<ActionResult<Response>> AddProgramToApplicantList(Guid? id);

        Task<ActionResult<Response>> ChangeProgramPriority(int priority, Guid? id);

        Task<ActionResult<List<ApplicantProgramDTO>>> GetApplicantPrograms();

        Task<ActionResult<List<FacultyDTO>>> GetFaculties();

        Task<ActionResult<List<ProgramDTO>>> GetListOfProgramsWithPaginationAndFiltering(ProgramsFilterQuery query);

        Task<ActionResult<Response>> RemoveProgramFromApplicantList(Guid? id);
    }
}
