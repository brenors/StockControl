using StockControl.Domain.Entities;

namespace StockControl.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}