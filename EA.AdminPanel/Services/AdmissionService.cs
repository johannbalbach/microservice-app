using EA.AdminPanel.Services.Interfaces;
using Enrollment.Domain.Models.Query;
using Shared.DTO;
using Shared.Models.DTO;
using System.Web;

namespace EA.AdminPanel.Services
{
    public class AdmissionService: IAdmissionService
    {
        private readonly HttpClient _httpClient;

        public AdmissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AdmissionWithPaginationInfo> GetAdmissionsWithFilter(AdmissionsFilterQuery query)
        {
            var queryString = BuildQueryString(query);
            var response = await _httpClient.GetAsync($"/api/enrollment/getListOfAdmissions{queryString}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<AdmissionWithPaginationInfo>();
        }

        private string BuildQueryString(AdmissionsFilterQuery query)
        {
            var queryParams = HttpUtility.ParseQueryString(string.Empty);

            if (query.Page.HasValue) queryParams["page"] = query.Page.Value.ToString();
            if (query.Size.HasValue) queryParams["size"] = query.Size.Value.ToString();
            if (query.ProgramId.HasValue) queryParams["programId"] = query.ProgramId.Value.ToString();
            if (query.Status.HasValue) queryParams["status"] = query.Status.Value.ToString();
            if (query.UnassignedOnly.HasValue) queryParams["unassignedOnly"] = query.UnassignedOnly.Value.ToString();
            if (query.FirstPriorityOnly.HasValue) queryParams["firstPriorityOnly"] = query.FirstPriorityOnly.Value.ToString();
            queryParams["sortBy"] = query.SortBy.ToString();

            return "?" + queryParams.ToString();
        }
    }
}
