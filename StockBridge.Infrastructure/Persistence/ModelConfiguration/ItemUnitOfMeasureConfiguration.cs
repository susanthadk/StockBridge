using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ItemUnitOfMeasureConfiguration : IEntityTypeConfiguration<ItemUnitOfMeasure>
{
    public void Configure(EntityTypeBuilder<ItemUnitOfMeasure> entity)
    {
        entity.ToTable("ItemUnitOfMeasure");

        entity.HasIndex(e => new { e.ItemId, e.UnitOfMeasureId }, "UQ_ItemUOM_Item_Unit").IsUnique();

        entity.Property(e => e.ConversionFactor).HasColumnType("decimal(18, 6)");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_ItemUOM_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_ItemUOM_IsActive");

        entity.HasOne(d => d.UnitOfMeasure).WithMany(p => p.ItemUnitOfMeasures)
            .HasForeignKey(d => d.UnitOfMeasureId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ItemUOM_Unit");
    }
}