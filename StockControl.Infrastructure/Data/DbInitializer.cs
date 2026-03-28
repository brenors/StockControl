using Microsoft.EntityFrameworkCore;
using StockControl.Domain.Entities;

namespace StockControl.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static ModelBuilder UseDataSeed(this ModelBuilder builder)
        {

            builder.Entity<User>().HasData(
                new User
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
                    Name = "Admin",
                    Email = "admin@admin.com",
                    PasswordHash = "123456",
                    Role = UserRole.Admin,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new User
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111113"),
                    Name = "Seller",
                    Email = "seller@admin.com",
                    PasswordHash = "123456",
                    Role = UserRole.Seller,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111112"),
                    Name = "Produto Teste 1",
                    Description = "Descrição teste",
                    Price = 100,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                 new Product
                 {
                     Id = new Guid("11111111-1111-1111-1111-111111111114"),
                     Name = "Produto Teste 2",
                     Description = "Outro produto",
                     Price = 200,
                     CreatedAt = new DateTime(2026, 1, 1)
                 }
            );

            return builder;
        }
    }
}
