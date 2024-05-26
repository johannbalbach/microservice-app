using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shared.JWT
{
    public static class JWTConfiguration
    {
        public const string Issuer = "https:/localhost:5012";
        public const string Audience = "Audience";
        public const string Key = "SOMEKEEEEEEEEEEEEEEEEEEEEEEYeeyeyeyey";
        public const int AccessLifeTime = 60;
        public const int RefreshLifeTime = AccessLifeTime * 24;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }

}
