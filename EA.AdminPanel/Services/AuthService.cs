using EA.AdminPanel.Models;
using EA.AdminPanel.Services.Interfaces;
using Shared.DTO;
using Shared.Models;
using System.Net.Http;

namespace EA.AdminPanel.Services
{
    public class AuthService: IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<Token> Login(LoginCredentials credentials)
        {
            var response = await _httpClient.PostAsJsonAsync("/user/login", credentials);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsAsync<Response>();
            Console.WriteLine(responseData);

            return TokenExtractor.ExtractToken(responseData.Message);
        }
    }
}
