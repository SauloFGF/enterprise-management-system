using System.Text;
using EnterpriseManagement.Application.Authentication;
using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Infrastructure.Authentication;
using EnterpriseManagement.Infrastructure.Persistence.Context;
using EnterpriseManagement.Infrastructure.Persistence.Seed;
using EnterpriseManagement.Infrastructure.Repositories;
using EnterpriseManagement.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContextPool<ApplicationDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

var jwtSection = builder.Configuration.GetSection("Jwt");

var jwtKey = jwtSection["key"] ?? throw new InvalidOperationException("Jwt Key not configured.");

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,

        ValidateAudience = true,

        ValidateIssuerSigningKey = true,

        ValidateLifetime = true,

        ValidIssuer = jwtSection["Issuer"],

        ValidAudience = jwtSection["Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

        ClockSkew = TimeSpan.FromSeconds(30)
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await UserSeed.SeedAsync(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

// app.UseHttpsRedirection(); // Desabilitado para testes locais

app.UseAuthentication();

app.UseAuthorization();

app.Run();