using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.BL.Services
{
    public class ExternalSystemService
    {
        private readonly HttpClient _httpClient;

        public ExternalSystemService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://1c-mockup.kreosoft.space/api/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("student:ny6gQnyn4ecbBrP9l1Fz")));
        }

        public async Task<string> GetEducationLevelsAsync()
        {
            var response = await _httpClient.GetAsync("dictionary/education_levels");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
