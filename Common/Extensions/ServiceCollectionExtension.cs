﻿using Common.ServiceBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Interfaces;
using Shared.JWT;
using Shared.Models.Enums;
using System.Text.Json.Serialization;
using User.Domain.Context;
using User.Domain.Entities;

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

            // Добавление Identity и конфигурации JWT-токенов
            services.AddIdentity<UserE, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = JWTConfiguration.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = JWTConfiguration.Audience,
                    ValidateAudience = true,
                    IssuerSigningKey = JWTConfiguration.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    LifetimeValidator = (before, expires, token, parameters) =>
                    {
                        var utcNow = DateTime.UtcNow;
                        return before <= utcNow && utcNow < expires;
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
                options.AddPolicy("ApplicantOrManager", p =>
                {
                    p.RequireAssertion(context =>
                    {
                        return context.User.HasClaim(claim => (claim.Type == "UserRole" && claim.Value == RoleEnum.Applicant.ToString())
                        || (claim.Type == "UserRole" && claim.Value == RoleEnum.Manager.ToString())
                        );
                    });
                });
                options.AddPolicy("Privileged", p =>
                {
                    p.RequireAssertion(context =>
                    {
                        return context.User.HasClaim(claim => (claim.Type == "UserRole" && claim.Value == RoleEnum.Manager.ToString())
                        || (claim.Type == "UserRole" && claim.Value == RoleEnum.MainManager.ToString() || (claim.Type == "UserRole" && claim.Value == RoleEnum.Admin.ToString()))
                        );
                    });
                });
            });

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddScoped<IRabbitMqService, RabbitMqService>();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("AuthConnection")));
        }

    }
}
