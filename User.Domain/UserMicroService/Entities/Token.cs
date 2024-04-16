using Microsoft.AspNetCore.Identity;
namespace User.Domain.Entities
{
    public class Token: IdentityUserToken<Guid>
    {
        public string RefreshToken { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
