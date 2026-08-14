using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
{
    public void Configure(EntityTypeBuilder<ItemType> entity)
    {
        entity.ToTable("ItemType");

        entity.HasIndex(e => e.ItemTypeCode, "UQ_ItemType_Code").IsUnique();

        entity.HasIndex(e => e.ItemTypeName, "UQ_ItemType_Name").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_ItemType_CreatedOn");
        entity.Property(e => e.Description).HasMaxLength(250);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_ItemType_IsActive");
        entity.Property(e => e.IsInventoryItem).HasDefaultValue(true, "DF_ItemType_IsInventoryItem");
        entity.Property(e => e.ItemTypeCode).HasMaxLength(20);
        entity.Property(e => e.ItemTypeName).HasMaxLength(50);
    }
}
