using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockControl.Domain.Entities;

namespace StockControl.Infrastructure.Maps
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
