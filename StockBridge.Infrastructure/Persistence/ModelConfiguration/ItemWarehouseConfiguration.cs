using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ItemWarehouseConfiguration : IEntityTypeConfiguration<ItemWarehouse>
{
    public void Configure(EntityTypeBuilder<ItemWarehouse> entity)
    {
        entity.ToTable("ItemWarehouse");

        entity.HasIndex(e => new { e.ItemId, e.WarehouseId }, "UQ_ItemWarehouse").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_ItemWarehouse_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_ItemWarehouse_IsActive");
        entity.Property(e => e.MaximumStockQuantity).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.MinimumStockQuantity).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.ReorderLevelQuantity).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.ReorderQuantity).HasColumnType("decimal(18, 3)");

        entity.HasOne(d => d.Warehouse).WithMany(p => p.ItemWarehouses)
            .HasForeignKey(d => d.WarehouseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ItemWarehouse_Warehouse");
    }
}