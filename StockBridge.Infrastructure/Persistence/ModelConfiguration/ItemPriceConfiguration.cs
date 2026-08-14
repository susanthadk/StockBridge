using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ItemPriceConfiguration : IEntityTypeConfiguration<ItemPrice>
{
    public void Configure(EntityTypeBuilder<ItemPrice> entity)
    {
        entity.ToTable("ItemPrice");

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_ItemPrice_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_ItemPrice_IsActive");
        entity.Property(e => e.PriceType).HasMaxLength(30);
        entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 4)");

        entity.HasOne(d => d.ItemUnitOfMeasure).WithMany(p => p.ItemPrices)
            .HasForeignKey(d => d.ItemUnitOfMeasureId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ItemPrice_ItemUOM");
    }
}