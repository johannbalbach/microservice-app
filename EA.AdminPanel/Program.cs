
using Common.Extensions;
using Common.Middleware;
using EA.AdminPanel.Helper;
using EA.AdminPanel.Services;
using EA.AdminPanel.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCommonServices();
builder.Services.AddAuth();

/*builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdmissionService, AdmissionService>();*/

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenHandler>();

builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5286");
});
builder.Services.AddHttpClient<IUserService, UserService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5012");
}).AddHttpMessageHandler<TokenHandler>();
builder.Services.AddHttpClient<IAdmissionService, AdmissionService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5027");
}).AddHttpMessageHandler<TokenHandler>();

/*builder.Services.AddHttpClient<IUserService, UserService>();
builder.Services.AddHttpClient<IAdmissionService, AdmissionService>();*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseMiddleware<TokenCatchMiddleware>();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["AccessToken"];
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Authorization = $"Bearer {token}";
    }

    await next();
});

app.UseExceptionHandler();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
