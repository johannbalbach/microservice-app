using Shared.Models.Enums;
using System.Security.Claims;

namespace Shared.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(string email, RoleEnum role);
        Task<string> GenerateRefreshToken();
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    }
}
