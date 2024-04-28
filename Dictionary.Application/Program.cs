using Common.Middleware;
using Dictionary.BL.MappingProfile;
using Dictionary.BL.Services;
using Dictionary.Domain.Context;
using Dictionary.Domain.Entities;
using Dictionary.Domain.Repository;
using Shared.Interfaces;
using Shared.JWT;
using Shared.Models.Enums;
using User.Domain.Context;
using User.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Common.Extensions;
using Common.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonServices();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<RabbitMqListener>();
builder.Services.AddScoped<IProgramRepository<UniversityProgram>, ProgramRepository<UniversityProgram>>();
builder.Services.AddScoped<IRepository<Faculty>, Repository<Faculty>>();
builder.Services.AddScoped<IRepository<EducationLevel>, Repository<EducationLevel>>();
builder.Services.AddScoped<IRepository<DocumentType>, Repository<DocumentType>>();

builder.Services.AddScoped<IDictionaryService, DictionaryService>();
builder.Services.AddScoped<IExternalSystemService, ExternalSystemService>();
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
