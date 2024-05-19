using EA.AdminPanel.Models;
using Shared.DTO;

namespace EA.AdminPanel.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDTO> GetProfileAsync();
        Task ChangePasswordAsync(string password);
    }
}
