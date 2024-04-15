using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using User.Domain.Context;

namespace Common.Middleware
{
    public class TokenCatchMiddleware
    {
        private readonly RequestDelegate _next;
        ILogger<TokenCatchMiddleware> _logger;
        public TokenCatchMiddleware(RequestDelegate next, ILogger<TokenCatchMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, AppDbContext dbContext)
        {
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            string? headerToken = httpContext.Request.Headers.Authorization;

            await _next(httpContext);

            if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                Console.WriteLine($"{headerToken}");
                string details = headerToken == null ? "Not Authenticated" : "Invalid Token";

                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                httpContext.Response.ContentType = "application/json";

                var problemDetails = new
                {
                    title = "Authorization Issue",
                    details = details
                };

                var json = JsonSerializer.Serialize(problemDetails);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
