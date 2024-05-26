using Microsoft.IdentityModel.Tokens;
using Shared.Models.Enums;
using Shared.Interfaces;
using Shared.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace User.BL.Services
{
    public class TokenService : ITokenService
    {
        public static ClaimsIdentity GetClaimsIdentity(string email, List<RoleEnum> userRoles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Добавление каждой роли как отдельного claim
            claims.AddRange(userRoles.Select(role => new Claim("UserRole", role.ToString())));

            return new ClaimsIdentity(claims);
        }

        public async Task<string> GenerateAccessToken(string email, List<RoleEnum> roles)
        {
            var secretKey = JWTConfiguration.GetSymmetricSecurityKey();
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new SecurityTokenDescriptor
            {
                Subject = GetClaimsIdentity(email, roles),
                Issuer = JWTConfiguration.Issuer,
                Audience = JWTConfiguration.Audience,
                Expires = DateTime.UtcNow.AddMinutes(JWTConfiguration.AccessLifeTime),
                SigningCredentials = signinCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokeOptions);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JWTConfiguration.GetSymmetricSecurityKey(),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
/*        public async Task<(string accessToken, string refreshToken)> GenerateTokens(string email, RoleEnum role = RoleEnum.Applicant)
        {
            var accessToken = await GenerateAccessToken(email, role);
            var refreshToken = await GenerateRefreshToken();
            return (accessToken, refreshToken);
        }*/
    }
}
