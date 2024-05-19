using System.Text.RegularExpressions;

namespace EA.AdminPanel.Models
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class TokenExtractor
    {
        public static Token ExtractToken(string tokenString)
        {
            var token = new Token();

            var accessTokenMatch = Regex.Match(tokenString, @"AccessToken:\s*(.+)");
            var refreshTokenMatch = Regex.Match(tokenString, @"RefreshToken:\s*(.+)");

            if (accessTokenMatch.Success)
            {
                token.AccessToken = accessTokenMatch.Groups[1].Value;
            }

            if (refreshTokenMatch.Success)
            {
                token.RefreshToken = refreshTokenMatch.Groups[1].Value;
            }

            return token;
        }
    }
}
