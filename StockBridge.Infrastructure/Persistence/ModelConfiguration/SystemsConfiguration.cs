using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SystemsConfiguration : IEntityTypeConfiguration<Systems>
{
    public void Configure(EntityTypeBuilder<Systems> entity)
    {
        entity.ToTable("Systems");

        entity.HasKey(e => e.SystemId);

        entity.HasIndex(e => new { e.SystemRecordType, e.SystemRecordNumber }, "UQ_System_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_System_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_System_IsActive");
        entity.Property(e => e.SystemAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.SystemAmount2).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.SystemDescription).HasMaxLength(40);
        entity.Property(e => e.SystemDescription2).HasMaxLength(50);
        entity.Property(e => e.SystemFromDate).HasColumnType("datetime");
        entity.Property(e => e.SystemRecordNumber).HasMaxLength(10);
        entity.Property(e => e.SystemToDate).HasColumnType("datetime");
        entity.Property(e => e.ValueAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.ValueAmount2).HasColumnType("decimal(18, 2)");
    }
}
