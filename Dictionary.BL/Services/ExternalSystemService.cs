using Dictionary.Domain.Entities;
using Dictionary.Domain.Repository;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.BL.Services
{
    public class ExternalSystemService: IExternalSystemService
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
        public async Task<string> GetDocumentTypesAsync()
        {
            var response = await _httpClient.GetAsync("dictionary/document_types");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> GetFacultiesAsync()
        {
            var response = await _httpClient.GetAsync("dictionary/faculties");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> GetProgramsAsync(int page, int size)
        {
            var response = await _httpClient.GetAsync($"dictionary/programs?page={page}&size={size}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
