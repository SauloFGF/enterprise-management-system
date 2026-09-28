using EnterpriseManagement.Application.Interfaces;
using EnterpriseManagement.Domain.Entities;
using EnterpriseManagement.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(w => w.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);

        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        context.Users.Update(user);

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        context.Users.Remove(user);

        await context.SaveChangesAsync();
    }
}
