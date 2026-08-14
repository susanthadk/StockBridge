using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> entity)
    {
        entity.ToTable("Item");

        entity.HasIndex(e => e.ItemCode, "UQ_Item_ItemCode").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())");
        entity.Property(e => e.GrossWeight).HasColumnType("decimal(18, 4)");
        entity.Property(e => e.Height).HasColumnType("decimal(18, 4)");
        entity.Property(e => e.IsActive).HasDefaultValue(true);
        entity.Property(e => e.IsPurchaseItem).HasDefaultValue(true);
        entity.Property(e => e.IsSaleItem).HasDefaultValue(true);
        entity.Property(e => e.IsStockItem).HasDefaultValue(true);
        entity.Property(e => e.ItemCode).HasMaxLength(30);
        entity.Property(e => e.ItemName).HasMaxLength(200);
        entity.Property(e => e.Length).HasColumnType("decimal(18, 4)");
        entity.Property(e => e.NetWeight).HasColumnType("decimal(18, 4)");
        entity.Property(e => e.ShortName).HasMaxLength(100);
        entity.Property(e => e.Width).HasColumnType("decimal(18, 4)");

        entity.HasOne(d => d.BaseUnitOfMeasure).WithMany(p => p.Items)
            .HasForeignKey(d => d.BaseUnitOfMeasureId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Item_BaseUnitOfMeasure");

        entity.HasOne(d => d.Brand).WithMany(p => p.Items)
            .HasForeignKey(d => d.BrandId)
            .HasConstraintName("FK_Item_Brand");

        entity.HasOne(d => d.ItemType).WithMany(p => p.Items)
            .HasForeignKey(d => d.ItemTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Item_ItemType");

        entity.HasOne(d => d.ProductHierarchy).WithMany(p => p.Items)
            .HasForeignKey(d => d.ProductHierarchyId)
            .HasConstraintName("FK_Item_ProductHierarchy");

        entity.HasOne(d => d.TaxCategory).WithMany(p => p.Items)
            .HasForeignKey(d => d.TaxCategoryId)
            .HasConstraintName("FK_Item_TaxCategory");
    }
}
