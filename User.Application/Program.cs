using Common.Extensions;
using Common.Middleware;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using User.BL.Consumers;
using User.BL.MappingProfile;
using User.BL.Services;
using User.Domain.Context;
using User.Domain.Entities;

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
    x.AddConsumer<GetUserConsumer>();
    x.AddConsumer<AddDocumentConsumer>();
    x.AddConsumer<DocumentRequestConsumer>();
});

builder.Services.AddCommonServices();
// Добавление Identity и конфигурации JWT-токенов
builder.Services.AddIdentity<UserE, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuth();

// Подключение к базе данных
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthConnection")));

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRequestsService, UserRequestService>();
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
