using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class MultiItemConfiguration : IEntityTypeConfiguration<MultiItem>
{
    public void Configure(EntityTypeBuilder<MultiItem> entity)
    {
        entity.ToTable("MultiItem");

        entity.HasIndex(e => new { e.StockCode, e.SellingPrice }, "UQ_MultiItem_BusinessKey").IsUnique();

        entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CreatedByUser).HasMaxLength(20);
        entity.Property(e => e.CreatedDate).HasColumnType("datetime");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_MultiItem_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_MultiItem_IsActive");
        entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.StockCode).HasMaxLength(8);
        entity.Property(e => e.StockDescriptioncription).HasMaxLength(40);
        entity.Property(e => e.StockOnHand)
            .HasDefaultValue(0m, "DF_MultiItem_StockOnHand")
            .HasColumnType("decimal(18, 3)");
        entity.Property(e => e.StockSizeCode).HasMaxLength(6);
        entity.Property(e => e.StockTypeCode).HasMaxLength(7);

        entity.HasOne(d => d.StockCodeNavigation).WithMany(p => p.MultiItems)
            .HasPrincipalKey(p => p.StockCode)
            .HasForeignKey(d => d.StockCode)
            .HasConstraintName("FK_MultiItem_Stock");

        entity.HasOne(d => d.StockTypeCodeNavigation).WithMany(p => p.MultiItems)
            .HasPrincipalKey(p => p.ItemCode)
            .HasForeignKey(d => d.StockTypeCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_MultiItem_Item");
    }
}
