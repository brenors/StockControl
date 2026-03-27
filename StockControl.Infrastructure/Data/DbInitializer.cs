using Microsoft.EntityFrameworkCore;
using StockControl.Domain.Entities;

namespace StockControl.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            // Se já tiver dados, não faz nada
            if (context.Users.Any())
                return;

            var users = new List<User>
        {
            new User
            {
                Name = "Admin",
                Email = "admin@admin.com",
                PasswordHash = "123456",
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Name = "Seller",
                Email = "seller@admin.com",
                PasswordHash = "123456",
                Role = UserRole.Seller,
                CreatedAt = DateTime.UtcNow
            }
        };

            var products = new List<Product>
        {
            new Product
            {
                Name = "Produto Teste 1",
                Description = "Descrição teste",
                Price = 100,
                CreatedAt = DateTime.UtcNow
            },
            new Product
            {
                Name = "Produto Teste 2",
                Description = "Outro produto",
                Price = 200,
                CreatedAt = DateTime.UtcNow
            }
        };

            await context.Users.AddRangeAsync(users);
            await context.Products.AddRangeAsync(products);

            await context.SaveChangesAsync();
        }
    }
}
