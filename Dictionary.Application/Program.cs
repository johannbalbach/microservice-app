using Dictionary.BL.MappingProfile;
using Dictionary.BL.Services;
using Dictionary.Domain.Context;
using Dictionary.Domain.Entities;
using Dictionary.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressMapClientErrors = true;
});
builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorize",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string> ()
        }
    });
});

// Подключение Identity с настройками для класса UserE
builder.Services.AddIdentity<UserE, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Конфигурация JWT-токенов
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = JWTConfiguration.Issuer,
        ValidateIssuer = true,
        ValidAudience = JWTConfiguration.Audience,
        ValidateAudience = true,
        IssuerSigningKey = JWTConfiguration.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        LifetimeValidator = (before, expires, token, parameters) =>
        {
            var utcNow = DateTime.UtcNow;
            return before <= utcNow && utcNow < expires;
        }
    };
});


// Добавление разрешений для ролей
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(RoleEnum.Applicant.ToString(), policy => policy.RequireClaim("UserRole", RoleEnum.Applicant.ToString()));
    options.AddPolicy(RoleEnum.Manager.ToString(), policy => policy.RequireClaim("UserRole", RoleEnum.Manager.ToString()));
    options.AddPolicy(RoleEnum.MainManager.ToString(), policy => policy.RequireClaim("UserRole", RoleEnum.MainManager.ToString()));
    options.AddPolicy(RoleEnum.Admin.ToString(), policy => policy.RequireClaim("UserRole", RoleEnum.Admin.ToString()));
    options.AddPolicy("ApplicantOrManager", p =>
    {
        p.RequireAssertion(context =>
        {
            return context.User.HasClaim(claim => (claim.Type == "UserRole" && claim.Value == RoleEnum.Applicant.ToString())
            || (claim.Type == "UserRole" && claim.Value == RoleEnum.Manager.ToString())
            );
        });
    });
    options.AddPolicy("Privileged", p =>
    {
        p.RequireAssertion(context =>
        {
            return context.User.HasClaim(claim => (claim.Type == "UserRole" && claim.Value == RoleEnum.Manager.ToString())
            || (claim.Type == "UserRole" && claim.Value == RoleEnum.MainManager.ToString() || (claim.Type == "UserRole" && claim.Value == RoleEnum.Admin.ToString()))
            );
        });
    });
});
*/
// Подключение к базе данных
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IRepository<UniversityProgram>, Repository<UniversityProgram>>();
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

/*app.UseAuthentication();

app.UseMiddleware<TokenCatchMiddleware>();

app.UseAuthorization();*/

app.UseExceptionHandler();

app.MapControllers();

app.Run();
