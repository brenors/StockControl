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
                    PasswordHash = "$2a$12$vNDPN.V8E8Gtmt.ZGk7SNebq0NN/7SBdPs5rkqx/5EBm7pD.BJro2",
                    Role = UserRole.Admin,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new User
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111113"),
                    Name = "Seller",
                    Email = "seller@admin.com",
                    PasswordHash = "$2a$12$vNDPN.V8E8Gtmt.ZGk7SNebq0NN/7SBdPs5rkqx/5EBm7pD.BJro2",
                    Role = UserRole.Seller,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111112"),
                    Name = "Bola de Futebol",
                    Description = "Bola oficial tamanho 5",
                    Price = 120,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Product
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111114"),
                    Name = "Chuteira Nike",
                    Description = "Chuteira para campo profissional",
                    Price = 350,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Product
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111115"),
                    Name = "Camiseta Esportiva",
                    Description = "Camiseta dry-fit para treino",
                    Price = 80,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Product
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111116"),
                    Name = "Luva de Goleiro",
                    Description = "Luva profissional com alta aderência",
                    Price = 150,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Product
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111117"),
                    Name = "Caneleira",
                    Description = "Caneleira leve para proteção",
                    Price = 60,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );

            return builder;
        }
    }
}
