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
        Task<ActionResult<Response>> AssignManagerToAdmission(Guid admissionId, Guid managerId);

        Task<ActionResult<Response>> AssignManagerToApplicant(Guid applicantId, Guid managerId);

        Task<ActionResult<Response>> EditAdmissionStatus(StatusEnum body, Guid id, string email);

        Task<ActionResult<List<AdmissionDTO>>> GetApplicantAdmissions(Guid id);

        Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting(AdmissionsFilterQuery query);

        Task<ActionResult<Response>> AddProgramToMyList(Guid id, string email);

        Task<ActionResult<Response>> ChangeProgramPriority(int priority, Guid id, string email);

        Task<ActionResult<List<AdmissionDTO>>> GetMyAdmissions(string email);

        Task<ActionResult<Response>> RemoveAdmissionFromApplicantList(Guid id, string email);
    }
}
