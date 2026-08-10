using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class GoodsReceiptTemporaryHeaderConfiguration : IEntityTypeConfiguration<GoodsReceiptTemporaryHeader>
{
    public void Configure(EntityTypeBuilder<GoodsReceiptTemporaryHeader> entity)
    {
        entity.ToTable("GoodsReceiptTemporaryHeader");

        entity.HasIndex(e => new { e.GoodsReceiptNumber, e.TerminalNumber }, "UQ_GoodsReceiptTemporaryHeader_BusinessKey").IsUnique();

        entity.Property(e => e.GoodsReceiptDate).HasColumnType("datetime");
        entity.Property(e => e.GoodsReceiptNumber).HasMaxLength(10);
        entity.Property(e => e.GoodsReceiptSite).HasMaxLength(4);
    }
}
