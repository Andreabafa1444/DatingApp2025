using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});
builder.Services.AddCors();
builder.Services.AddSingleton<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins(
        "http://localhost:4200",
        "https://localhost:4200"
    ));

app.MapControllers();

app.Run();