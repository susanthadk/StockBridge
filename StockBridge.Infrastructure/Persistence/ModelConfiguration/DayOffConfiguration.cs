using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class DayOffConfiguration : IEntityTypeConfiguration<DayOff>
{
    public void Configure(EntityTypeBuilder<DayOff> entity)
    {
        entity.ToTable("DayOff");

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_DayOff_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_DayOff_IsActive");
        entity.Property(e => e.OffDate).HasColumnType("datetime");
        entity.Property(e => e.OffNumber).HasMaxLength(10);
        entity.Property(e => e.OffTimee).HasColumnType("datetime");
    }
}
