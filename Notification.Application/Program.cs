using Common.Extensions;
using Common.Middleware;
using Notification.BL.Services;
using Shared.Consts;
using Shared.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCommonServices();
builder.Services.AddAuth();

builder.Services.Configure<EmailConfiguration>(
        builder
            .Configuration
            .GetSection(nameof(EmailConfiguration))
    );
builder.Services.AddScoped<INotificationService, NotificationService>();

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
