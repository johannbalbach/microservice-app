using Common.Extensions;
using Common.Middleware;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
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
});

builder.Services.AddCommonServices();
// ���������� Identity � ������������ JWT-�������
builder.Services.AddIdentity<UserE, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuth();

// ����������� � ���� ������
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

app.Use(async (context, next) =>
{
    if (context.Request.Headers.ContainsKey("Authorization")) //AuthenticationHelper.ValidateToken(context.Request.Headers["Authorization"]
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5012/");
            var response = await client.GetAsync("api/user/check-token");
            if (!response.IsSuccessStatusCode)
            {
                context.Response.StatusCode = 401;
                return;
            }
        }
    }

    await next();
});

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
