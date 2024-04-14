using Shared.Models.Enums;
using System.Security.Claims;

namespace Shared.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string email, RoleEnum role);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
