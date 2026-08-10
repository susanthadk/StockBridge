using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class StockOnHandSummaryConfiguration : IEntityTypeConfiguration<StockOnHandSummary>
{
    public void Configure(EntityTypeBuilder<StockOnHandSummary> entity)
    {
        entity.ToTable("StockOnHandSummary");

        entity.HasIndex(e => e.ItemNumber, "UQ_StockOnHandSummary_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_StockOnHandSummary_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_StockOnHandSummary_IsActive");
        entity.Property(e => e.ItemNumber).HasMaxLength(8);
    }
}
