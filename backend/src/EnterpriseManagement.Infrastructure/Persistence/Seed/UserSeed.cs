using EnterpriseManagement.Domain.Entities;
using EnterpriseManagement.Infrastructure.Persistence.Context;
using EnterpriseManagement.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Persistence.Seed;

public static class UserSeed
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Users.AnyAsync())
        {
            return;
        }

        var passwordHasher = new PasswordHasher();

        // Passwords come from environment variables in Development only.
        // Never hardcode passwords in source code.
        var adminPassword = Environment.GetEnvironmentVariable("SEED_ADMIN_PASSWORD")
            ?? throw new InvalidOperationException("SEED_ADMIN_PASSWORD environment variable is not set.");
        var operatorPassword = Environment.GetEnvironmentVariable("SEED_OPERATOR_PASSWORD")
            ?? throw new InvalidOperationException("SEED_OPERATOR_PASSWORD environment variable is not set.");

        var users = new List<User>
        {
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Admin User",
                Email = "admin@example.com",
                PasswordHash = passwordHasher.Hash(adminPassword),
                Role = "Admin",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Operator User",
                Email = "operator@example.com",
                PasswordHash = passwordHasher.Hash(operatorPassword),
                Role = "Operator",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }
}
