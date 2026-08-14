using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ItemSupplierConfiguration : IEntityTypeConfiguration<ItemSupplier>
{
    public void Configure(EntityTypeBuilder<ItemSupplier> entity)
    {
        entity.ToTable("ItemSupplier");

        entity.HasIndex(e => new { e.ItemId, e.SupplierId }, "UQ_ItemSupplier").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_ItemSupplier_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_ItemSupplier_IsActive");
        entity.Property(e => e.MinimumOrderQuantity).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.OrderMultipleQuantity).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18, 4)");
        entity.Property(e => e.SupplierItemCode).HasMaxLength(50);

        entity.HasOne(d => d.PurchaseUnitOfMeasure).WithMany(p => p.ItemSuppliers)
            .HasForeignKey(d => d.PurchaseUnitOfMeasureId)
            .HasConstraintName("FK_ItemSupplier_PurchaseUOM");

        entity.HasOne(d => d.Supplier).WithMany(p => p.ItemSuppliers)
            .HasForeignKey(d => d.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ItemSupplier_Supplier");
    }
}