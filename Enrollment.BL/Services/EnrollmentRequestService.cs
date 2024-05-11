using Enrollment.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace Enrollment.BL.Services
{
    public class EnrollmentRequestService: IEnrollmentRequestsService
    {
        private readonly AppDbContext _context;
        public EnrollmentRequestService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<bool> CheckManagerAssign(Guid ApplicantId, Guid ManagerId)
        {
            var admissions = await _context.Admissions.FirstOrDefaultAsync(a => a.ApplicantId == ApplicantId && a.ManagerId == ManagerId && a.Status != Shared.Models.Enums.StatusEnum.Closed);

            if (admissions  == null)
                return false;
            return true;
        }
    }
}
