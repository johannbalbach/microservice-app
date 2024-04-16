using Enrollment.Domain.Models.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Interfaces;
using Shared.Models;
using Shared.Models.DTO;
using Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment.BL.Services
{
    public class EnrollmentService: IEnrollmentService
    {
        public async Task<ActionResult<Response>> AssignManagerToAdmission(Guid? admissionId)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> AssignManagerToApplicant(Guid? applicantId)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> EditAdmissionStatus(StatusEnum body, Guid? id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<List<AdmissionDTO>>> GetApplicantAdmissions(Guid? id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting(AdmissionsFilterQuery query)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> AddProgramToApplicantList(Guid? id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> ChangeProgramPriority(int priority, Guid? id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<List<ApplicantProgramDTO>>> GetApplicantPrograms()
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> RemoveProgramFromApplicantList(Guid? id)
        {
            throw new NotImplementedException();
        }
    }
}
