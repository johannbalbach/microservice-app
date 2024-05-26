using Enrollment.Domain.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

namespace Enrollment.BL.Services
{
    public class EnrollmentRequestService: IEnrollmentRequestsService
    {
        private readonly AppDbContext _context;
        public EnrollmentRequestService(AppDbContext context, IBus bus) 
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
        public async Task<List<Guid>> GetApplicantProgramsId(Guid ApplicantId)
        {
            var admissions = await _context.Admissions.Where(a => a.ApplicantId == ApplicantId).ToListAsync();

            if (admissions == null || admissions.Count == 0)
                return new List<Guid>();
            List<Guid> guids = new List<Guid>();

            admissions.ForEach(a => guids.Add(a.ProgramId));
            return guids;
        }
    }
}
