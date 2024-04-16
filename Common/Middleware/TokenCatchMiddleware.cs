using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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

        public async Task InvokeAsync(HttpContext httpContext)
        {
            string? headerToken = httpContext.Request.Headers.Authorization;

            await _next(httpContext);
            Console.WriteLine(httpContext.Request.Headers.Authorization);

            if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
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
