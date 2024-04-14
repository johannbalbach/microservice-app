﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shared.JWT
{
    public class JWTConfiguration
    {
        public const string Issuer = "Issuer";
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
