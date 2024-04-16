using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Models;
using Shared.Models.Enums;
using User.Domain.Context;

namespace Common.Middleware
{
    public class RoleMiddleware
    {
        private readonly RequestDelegate _next;
        ILogger<RoleMiddleware> _logger;
        public RoleMiddleware(RequestDelegate next, ILogger<RoleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, AuthDbContext dbContext)
        {
            var endpoint = httpContext.GetEndpoint();
            var endpointAuthorizeData = endpoint?.Metadata.GetOrderedMetadata<IAuthorizeData>();
            string? headerToken = httpContext.Request.Headers.Authorization;

            var tokenValue = headerToken?.Substring(7);
            var userEmailClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            var userRoleClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "UserRole")?.Value;
            var userFromDB = dbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmailClaim).Result;

            if (userFromDB != null)
            {
                UserChecker.UserEmail = userFromDB.Email;
                UserChecker.UserId = userFromDB.Id;
            }

            await _next(httpContext);
        }
    }
}
