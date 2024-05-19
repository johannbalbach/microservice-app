using EA.AdminPanel.Models;
using Shared.DTO;

namespace EA.AdminPanel.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Token> Login(LoginCredentials credentials);
    }
}
