using Enrollment.Domain.Models;
using Enrollment.Domain.Models.DTO;
using Enrollment.Domain.Models.Enum;
using Enrollment.Domain.Models.Query;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.Domain.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task<ActionResult<Response>> AssignManagerToAdmission(Guid? admissionId);

        Task<ActionResult<Response>> AssignManagerToApplicant(Guid? applicantId);

        Task<ActionResult<Response>> EditAdmissionStatus(StatusEnum body, Guid? id);

        Task<ActionResult<List<AdmissionDTO>>> GetApplicantAdmissions(Guid? id);

        Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting(AdmissionsFilterQuery query);
    }
}
