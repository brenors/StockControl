using Microsoft.EntityFrameworkCore;
using StockControl.Application.Interfaces;
using StockControl.Domain.Entities;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }
}