using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using BookstoreApplication.Infrastructure;
using BookstoreApplication.Middleware;
using BookstoreApplication.Models;
using BookstoreApplication.Profiles;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using BookstoreApplication.Utils;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// SERILOG LOGGING KONFIGURACIJA
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// POSTGRESQL + ENTITY FRAMEWORK KONFIGURACIJA
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.ConfigureWarnings(warnings =>
        warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});

// MONGODB KONFIGURACIJA 
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDb");
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<IMongoDatabase>(serviceProvider =>
{
    var client = serviceProvider.GetService<IMongoClient>();
    var databaseName = builder.Configuration.GetSection("MongoDatabase:DatabaseName").Value;
    return client.GetDatabase(databaseName);
});

// IDENTITY KONFIGURACIJA
// (ASP.NET Core Identity za autentifikaciju)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
});

// AUTENTIFIKACIJA (JWT + GOOGLE OAUTH)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        RoleClaimType = ClaimTypes.Role
    };
})
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"];
    options.ClientSecret = builder.Configuration["Google:ClientSecret"];
    options.CallbackPath = "/signin-google";

    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");

    options.Events.OnCreatingTicket = async context =>
    {
        var email = context.Principal.FindFirstValue(ClaimTypes.Email);
        var name = context.Principal.FindFirstValue(ClaimTypes.Name);

        Console.WriteLine($"Google user authenticated: {email}, {name}");
    };
});

// AUTOMAPPER KONFIGURACIJA
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<BookProfile>();
});

// UNIT OF WORK PATTERN
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// SERVISI (Business Logic Layer)
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IVolumeService, VolumeService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAwardService, AwardService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// COMIC VINE API CONNECTION
builder.Services.AddScoped<IComicVineConnection, ComicVineConnection>();
builder.Services.AddHttpClient<ComicVineConnection>();

// REPOSITORIES

// MONGODB REPOSITORIES (za stripove i main business logic)
builder.Services.AddScoped<IIssueRepository, IssueMongoRepository>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAwardRepository, AwardRepository>();

// MIDDLEWAR
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// CONTROLLERS + JSON KONFIGURACIJA
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// SWAGGER DOKUMENTACIJA
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookstoreApplication API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert JWT token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// CORS KONFIGURACIJA
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

var app = builder.Build();

// DATABASE SEEDING (za PostgreSQL Identity tabele)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

// MIDDLEWARE PIPELINE
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Development environment konfiguracija
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Request pipeline
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();