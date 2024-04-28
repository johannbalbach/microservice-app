using Common.Extensions;
using Common.Middleware;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Interfaces;
using Shared.JWT;
using Shared.Models.Enums;
using System.Data;
using System.Text.Json.Serialization;
using User.BL.MappingProfile;
using User.BL.Services;
using User.Domain.Context;
using User.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonServices();

// Подключение к базе данных
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthConnection")));

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseMiddleware<TokenCatchMiddleware>();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
