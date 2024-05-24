using Common.Middleware;
using Dictionary.BL.MappingProfile;
using Dictionary.BL.Services;
using Dictionary.Domain.Context;
using Dictionary.Domain.Entities;
using Dictionary.Domain.Repository;
using Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Common.Extensions;
using MassTransit;
using Dictionary.BL.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
    x.AddConsumer<ProgramExistConsumer>();
    x.AddConsumer<EntityExistConsumer>();
});

builder.Services.AddCommonServices();
builder.Services.AddAuth();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProgramRepository<UniversityProgram>, ProgramRepository<UniversityProgram>>();
builder.Services.AddScoped<IRepository<Faculty>, Repository<Faculty>>();
builder.Services.AddScoped<IRepository<EducationLevel>, Repository<EducationLevel>>();
builder.Services.AddScoped<IRepository<DocumentType>, Repository<DocumentType>>();

builder.Services.AddScoped<IDictionaryService, DictionaryService>();
builder.Services.AddTransient<IExternalSystemService, ExternalSystemService>();
builder.Services.AddScoped<IDictionaryRequestService, DictionaryRequestService>();
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
