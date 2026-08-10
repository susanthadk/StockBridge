using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class AreaRouteConfiguration : IEntityTypeConfiguration<AreaRoute>
{
    public void Configure(EntityTypeBuilder<AreaRoute> entity)
    {
        entity.ToTable("AreaRoute");

        entity.HasIndex(e => e.AreaCode, "UQ_AreaRoute_BusinessKey").IsUnique();

        entity.Property(e => e.AreaCode)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.AreaName)
            .HasMaxLength(40)
            .IsUnicode(false);
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_AreaRoute_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_AreaRoute_IsActive");
        entity.Property(e => e.ShortName)
            .HasMaxLength(3)
            .IsUnicode(false);
    }
}
