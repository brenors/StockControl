using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockControl.Domain.Entities;

namespace StockControl.Infrastructure.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasConversion(
                    x => x.ToString(),
                    x => Enum.Parse<UserRole>(x))
                .HasMaxLength(6)
                .IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
