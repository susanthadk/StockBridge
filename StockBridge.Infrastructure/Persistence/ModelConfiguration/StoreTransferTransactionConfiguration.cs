using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class StoreTransferTransactionConfiguration : IEntityTypeConfiguration<StoreTransferTransaction>
{
    public void Configure(EntityTypeBuilder<StoreTransferTransaction> entity)
    {
        entity.ToTable("StoreTransferTransaction");

        entity.HasIndex(e => new { e.GoodsOutNoteNumber, e.GoodsInNoteNumber, e.ItemCode, e.StockCode }, "UQ_StoreTransferTransaction_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_StoreTransferTransaction_CreatedOn");
        entity.Property(e => e.GoodsInNoteDate).HasColumnType("datetime");
        entity.Property(e => e.GoodsInNoteNumber).HasMaxLength(15);
        entity.Property(e => e.GoodsInNoteQuantity).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.GoodsInNoteValue).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.GoodsOutNoteDate).HasColumnType("datetime");
        entity.Property(e => e.GoodsOutNoteNumber).HasMaxLength(15);
        entity.Property(e => e.GoodsOutNoteQuantity).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.GoodsOutNoteValue).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_StoreTransferTransaction_IsActive");
        entity.Property(e => e.ItemCode).HasMaxLength(7);
        entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.StockCode).HasMaxLength(8);

        entity.HasOne(d => d.StockCodeNavigation).WithMany(p => p.StoreTransferTransactions)
            .HasPrincipalKey(p => p.StockCode)
            .HasForeignKey(d => d.StockCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_StoreTransferTransaction_Stock");
    }
}
