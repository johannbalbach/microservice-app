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
        public static ClaimsIdentity GetClaimsIdentity(string email, RoleEnum userRole)
        {
            return new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("UserRole", userRole.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });
        }

        public string GenerateAccessToken(string email, RoleEnum role = RoleEnum.ApplicantEnum)
        {
            var secretKey = JWTConfiguration.GetSymmetricSecurityKey();
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new SecurityTokenDescriptor
            {
                Subject = GetClaimsIdentity(email, role),
                Issuer = JWTConfiguration.Issuer,
                Audience = JWTConfiguration.Audience,
                Expires = DateTime.Now.AddMinutes(JWTConfiguration.AccessLifeTime),
                SigningCredentials = signinCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokeOptions);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
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
    }
}
