using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockControl.Domain.Entities;

namespace StockControl.Infrastructure.Maps
{
    public class StockMap : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.InvoiceNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(p => p.Stocks)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
