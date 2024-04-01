using EnrollmentMicroService.Models.DTO;
using EnrollmentMicroService.Models.Enum;
using EnrollmentMicroService.Models.Query;
using EnrollmentSystem.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentMicroService.Services.Interfaces
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
