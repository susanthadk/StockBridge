using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class HotItemConfiguration : IEntityTypeConfiguration<HotItem>
{
    public void Configure(EntityTypeBuilder<HotItem> entity)
    {
        entity.ToTable("HotItem");

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_HotItem_CreatedOn");
        entity.Property(e => e.Descriptioncription).HasMaxLength(50);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_HotItem_IsActive");
        entity.Property(e => e.ItemCode).HasMaxLength(7);
        entity.Property(e => e.Quantity)
            .HasDefaultValue(0m, "DF_HotItem_Quantity")
            .HasColumnType("decimal(18, 2)");
    }
}
