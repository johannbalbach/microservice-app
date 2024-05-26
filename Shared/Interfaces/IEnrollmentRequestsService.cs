using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IEnrollmentRequestsService
    {
        Task<bool> CheckManagerAssign(Guid ApplicantId, Guid ManagerId);
        Task<List<Guid>> GetApplicantProgramsId(Guid ApplicantId);
    }
}
