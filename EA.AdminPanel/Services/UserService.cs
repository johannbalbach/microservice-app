﻿using EA.AdminPanel.Models;
using EA.AdminPanel.Services.Interfaces;
using Shared.DTO;
using Shared.Models;

namespace EA.AdminPanel.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserProfileDTO> GetProfileAsync()
        {
            var response = await _httpClient.GetAsync("/api/user/getProfile");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<UserProfileDTO>();
        }

        public async Task ChangePasswordAsync(string password)
        {
            var response = await _httpClient.PostAsJsonAsync("/user/changePassword", password);
            response.EnsureSuccessStatusCode();
        }
    }
}
