using Shared.Models.Enums;
using System.Security.Claims;

namespace Shared.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(string email, List<RoleEnum> roles);
        Task<string> GenerateRefreshToken();
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    }
}
