using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class GoodsReceiptTemporaryDetailConfiguration : IEntityTypeConfiguration<GoodsReceiptTemporaryDetail>
{
    public void Configure(EntityTypeBuilder<GoodsReceiptTemporaryDetail> entity)
    {
        entity.ToTable("GoodsReceiptTemporaryDetail");

        entity.HasIndex(e => new { e.GoodsReceiptNumber, e.TerminalNumber, e.ItmType }, "UQ_GoodsReceiptTemporaryDetail_BusinessKey").IsUnique();

        entity.Property(e => e.GoodsReceiptDate).HasColumnType("datetime");
        entity.Property(e => e.GoodsReceiptNumber).HasMaxLength(10);
        entity.Property(e => e.GoodsReceiptQuantity).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.GoodsReceiptSellingPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.ItmType).HasMaxLength(7);

        entity.HasOne(d => d.GoodsReceiptTemporaryHeader).WithMany(p => p.GoodsReceiptTemporaryDetails)
            .HasPrincipalKey(p => new { p.GoodsReceiptNumber, p.TerminalNumber })
            .HasForeignKey(d => new { d.GoodsReceiptNumber, d.TerminalNumber })
            .HasConstraintName("FK_GoodsReceiptTemporaryDetail_GoodsReceiptTemporaryHeader");
    }
}
