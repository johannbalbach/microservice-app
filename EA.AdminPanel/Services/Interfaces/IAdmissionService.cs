using Enrollment.Domain.Models.Query;
using Shared.DTO;
using Shared.Models.DTO;

namespace EA.AdminPanel.Services.Interfaces
{
    public interface IAdmissionService
    {
        Task<AdmissionWithPaginationInfo> GetAdmissionsWithFilter(AdmissionsFilterQuery query);
    }
}
