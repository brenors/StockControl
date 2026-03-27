using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockControl.Domain.Entities;

namespace StockControl.Infrastructure.Maps
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerDocument)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.SellerName)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
