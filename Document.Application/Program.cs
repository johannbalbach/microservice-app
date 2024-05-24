using Document.BL.Services;
using Document.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using Common.Extensions;
using MassTransit;
using Common.ServiceBus;
using Common.Middleware;

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
});
builder.Services.AddCommonServices();
builder.Services.AddAuth();

builder.Services.AddDbContext<DocumentContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDocumentService, DocumentsService>();
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.Use(async (context, next) =>
{
    if (context.Request.Headers.ContainsKey("Authorization")) //AuthenticationHelper.ValidateToken(context.Request.Headers["Authorization"]
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5012/");
            var response = await client.GetAsync("api/user/check-token");
        }
    }

    await next();
});

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();