﻿using Common.Helpers;
using MassTransit;
using MassTransit.AspNetCoreIntegration.HealthChecks;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Interfaces;
using Shared.JWT;
using Shared.Models.Enums;
using System.Text.Json.Serialization;
using User.Domain.Context;

namespace Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            // Добавление контроллеров с настройками JSON и API Behavior
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });
            services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddEndpointsApiExplorer();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("AuthConnection")));
        }

        public static void AddAuth(this IServiceCollection services)
        {
            // Добавление Swagger
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Authorize",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string> ()
                }
            });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.UseSecurityTokenValidators = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = JWTConfiguration.Issuer,
                    ValidateIssuer = false,
                    ValidAudience = JWTConfiguration.Audience,
                    ValidateAudience = false,
                    IssuerSigningKey = JWTConfiguration.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = false,
                    ValidateLifetime = false
                };
                jwtOptions.TokenHandlers.Add(new BearerTokenHandler());
                jwtOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        Console.WriteLine($"HttpContext request headers: {context.HttpContext.Request.Headers.Authorization}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully.");
                        Console.WriteLine($"HttpContext request headers: {context.HttpContext.Request.Headers.Authorization}");
                        return Task.CompletedTask;
                    }
                };
            });

            // Добавление разрешений для ролей
            services.AddAuthorization(options =>
            {
                options.AddPolicy(RoleEnum.Applicant.ToString(), policy => policy.RequireClaim("UserRole", RoleEnum.Applicant.ToString()));
                options.AddPolicy(RoleEnum.Manager.ToString(), policy => policy.RequireClaim("UserRole", RoleEnum.Manager.ToString()));
                options.AddPolicy(RoleEnum.MainManager.ToString(), policy => policy.RequireClaim("UserRole", RoleEnum.MainManager.ToString()));
                options.AddPolicy(RoleEnum.Admin.ToString(), policy => policy.RequireClaim("UserRole", RoleEnum.Admin.ToString()));

                options.AddPolicy("ApplicantOrManager", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasClaim(claim => claim.Type == "UserRole" &&
                               (claim.Value == RoleEnum.Applicant.ToString() || claim.Value == RoleEnum.Manager.ToString()));
                    });
                });

                options.AddPolicy("MainManagerOrAdmin", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasClaim(claim => claim.Type == "UserRole" &&
                               (claim.Value == RoleEnum.MainManager.ToString() || claim.Value == RoleEnum.Admin.ToString()));
                    });
                });

                options.AddPolicy("Privileged", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasClaim(claim => claim.Type == "UserRole" &&
                               (claim.Value == RoleEnum.Manager.ToString() ||
                                claim.Value == RoleEnum.MainManager.ToString() ||
                                claim.Value == RoleEnum.Admin.ToString()));
                    });
                });
            });
        }
    }
}
