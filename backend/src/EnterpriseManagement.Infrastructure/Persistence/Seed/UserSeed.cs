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

        var users = new List<User>
        {
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Admin User",
                Email = "admin@example.com",
                PasswordHash = passwordHasher.Hash("Admin@123"),
                Role = "Admin",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Test User",
                Email = "test@example.com",
                PasswordHash = passwordHasher.Hash("Test@123"),
                Role = "User",
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }
}
