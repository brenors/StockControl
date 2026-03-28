using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StockControl.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=StockControl;User=sa;Password=Senha@123!;TrustServerCertificate=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
