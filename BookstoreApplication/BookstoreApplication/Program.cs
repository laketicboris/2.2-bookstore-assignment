using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;  // DODAJ OVO
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<PublisherRepository>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<AwardRepository>();

// DODAJ OVE 4 LINIJE:
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<PublisherService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<AwardService>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DODAJTE CORS KONFIGURACIJU
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(); // DODAJTE OVU LINIJU
app.UseAuthorization();
app.MapControllers();

app.Run();