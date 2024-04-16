using Enrollment.Domain.Models.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Models;
using Shared.Models.DTO;
using Shared.Models.Enums;

namespace Shared.Interfaces
{
    public interface IEnrollmentService
    {
        Task<ActionResult<Response>> AssignManagerToAdmission(Guid? admissionId);

        Task<ActionResult<Response>> AssignManagerToApplicant(Guid? applicantId);

        Task<ActionResult<Response>> EditAdmissionStatus(StatusEnum body, Guid? id);

        Task<ActionResult<List<AdmissionDTO>>> GetApplicantAdmissions(Guid? id);

        Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting(AdmissionsFilterQuery query);

        Task<ActionResult<Response>> AddProgramToApplicantList(Guid? id);

        Task<ActionResult<Response>> ChangeProgramPriority(int priority, Guid? id);

        Task<ActionResult<List<ApplicantProgramDTO>>> GetApplicantPrograms();

        Task<ActionResult<Response>> RemoveProgramFromApplicantList(Guid? id);
    }
}
