/*using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.JWT;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

new WebHostBuilder ()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("routes.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        // Добавление сервисов аутентификации JWT, если требуется
        if (context.Configuration["authenticationService:path"] != null)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = JWTConfiguration.GetSymmetricSecurityKey()
                    };
                });
        }
        services.AddControllers();
    })
    .Configure(app =>
    {
        // Добавление маршрутизации и аутентификации, если требуется
        var routes = app.ApplicationServices.GetRequiredService<IConfiguration>().GetSection("routes").Get<RoutesConfiguration>();
        if (routes != null)
        {
            app.UseRouting();

            if (routes.AuthenticationService != null)
            {
                app.UseAuthentication();
            }

            app.UseEndpoints(endpoints =>
            {
                foreach (var route in routes.Routes)
                {
                    if (route.Destination.RequiresAuthentication.ToLower() == "true")
                    {
                        endpoints.(proxyPipeline =>
                        {
                            proxyPipeline.UseAuthentication();
                            proxyPipeline.UseHttpMetrics();
                            proxyPipeline.UseProxyLoadBalancing();
                            proxyPipeline.UseForwardedHeaders();
                            proxyPipeline.Use((context, next) =>
                            {
                                context.Request.Path = route.Destination.Path + context.Request.Path;
                                return next();
                            });
                            proxyPipeline.UseProxyLoadBalancing();
                            proxyPipeline.UseHttpMetrics();
                            proxyPipeline.UseHttpActivityExporter();
                        });
                    }
                    else
                    {
                        endpoints.MapReverseProxy(proxyPipeline =>
                        {
                            proxyPipeline.UseHttpMetrics();
                            proxyPipeline.UseProxyLoadBalancing();
                            proxyPipeline.UseForwardedHeaders();
                            proxyPipeline.Use((context, next) =>
                            {
                                context.Request.Path = route.Destination.Path + context.Request.Path;
                                return next();
                            });
                            proxyPipeline.UseProxyLoadBalancing();
                            proxyPipeline.UseHttpMetrics();
                            proxyPipeline.UseHttpActivityExporter();
                        });
                    }
                }
            });
        }
    });
*/