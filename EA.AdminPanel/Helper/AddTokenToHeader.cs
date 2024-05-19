using Microsoft.AspNetCore.Mvc.Filters;

namespace EA.AdminPanel.Helper
{
    public class AddTokenToHeaderAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddTokenToHeaderAttribute(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];

            if (!string.IsNullOrEmpty(accessToken))
            {
                context.HttpContext.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }
        }
    }
}
