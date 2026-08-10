using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class MainLocationConfiguration : IEntityTypeConfiguration<MainLocation>
{
    public void Configure(EntityTypeBuilder<MainLocation> entity)
    {
        entity.ToTable("MainLocation");

        entity.HasIndex(e => e.MainLocCode, "UQ_MainLocation_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_MainLocation_CreatedOn");
        entity.Property(e => e.DatabaseName).HasMaxLength(50);
        entity.Property(e => e.DatabasePassword).HasMaxLength(50);
        entity.Property(e => e.DatabaseServer).HasMaxLength(50);
        entity.Property(e => e.DatabaseUser).HasMaxLength(50);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_MainLocation_IsActive");
        entity.Property(e => e.LinkedToCpu).HasColumnName("LinkedToCPU");
        entity.Property(e => e.LocType)
            .HasMaxLength(1)
            .IsUnicode(false);
        entity.Property(e => e.Location)
            .HasMaxLength(50)
            .IsUnicode(false);
        entity.Property(e => e.LocationActiveFlag)
            .HasMaxLength(1)
            .IsUnicode(false);
        entity.Property(e => e.MainLocCode)
            .HasMaxLength(20)
            .IsUnicode(false);
    }
}
