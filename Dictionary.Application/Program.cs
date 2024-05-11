using Common.Middleware;
using Dictionary.BL.MappingProfile;
using Dictionary.BL.Services;
using Dictionary.Domain.Context;
using Dictionary.Domain.Entities;
using Dictionary.Domain.Repository;
using Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonServices();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
