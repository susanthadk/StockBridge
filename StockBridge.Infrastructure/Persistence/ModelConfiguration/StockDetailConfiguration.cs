using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class StockDetailConfiguration : IEntityTypeConfiguration<StockDetail>
{
    public void Configure(EntityTypeBuilder<StockDetail> entity)
    {
        entity.ToTable("StockDetail");

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_StockDetail_CreatedOn");
        entity.Property(e => e.EnDate).HasColumnType("datetime");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_StockDetail_IsActive");
        entity.Property(e => e.OpDate).HasColumnType("datetime");
        entity.Property(e => e.PhyQuantity).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.PhyValue).HasColumnType("decimal(18, 2)");
    }
}
