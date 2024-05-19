using Common.Extensions;
using Ocelot.DependencyInjection;
using Common.Middleware;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCommonServices();
builder.Services.AddAuth();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

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

await app.UseOcelot();

app.MapControllers();

app.Run();
