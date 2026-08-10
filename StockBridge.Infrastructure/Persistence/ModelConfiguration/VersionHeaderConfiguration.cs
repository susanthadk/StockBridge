using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class VersionHeaderConfiguration : IEntityTypeConfiguration<VersionHeader>
{
    public void Configure(EntityTypeBuilder<VersionHeader> entity)
    {
        entity.ToTable("VersionHeader");

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_VersionHeader_CreatedOn");
        entity.Property(e => e.Date).HasColumnType("datetime");
        entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_VersionHeader_IsActive");
        entity.Property(e => e.Receipt).HasMaxLength(1);
        entity.Property(e => e.Sales).HasMaxLength(1);
        entity.Property(e => e.SalesReturn).HasMaxLength(1);
        entity.Property(e => e.Stock).HasMaxLength(1);
        entity.Property(e => e.Version).HasMaxLength(10);
    }
}
