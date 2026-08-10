using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class StockVarianceConfiguration : IEntityTypeConfiguration<StockVariance>
{
    public void Configure(EntityTypeBuilder<StockVariance> entity)
    {
        entity.ToTable("StockVariance");

        entity.HasIndex(e => new { e.StockCode, e.AdjDate }, "UQ_StockVariance_BusinessKey").IsUnique();

        entity.Property(e => e.AdjDate).HasColumnType("datetime");
        entity.Property(e => e.AfterStockAdjustment)
            .HasDefaultValue(0m, "DF_StockVariance_AfterStockAdjustment")
            .HasColumnType("decimal(18, 0)");
        entity.Property(e => e.BeforeStockAdjustment)
            .HasDefaultValue(0m, "DF_StockVariance_BeforeStockAdjustment")
            .HasColumnType("decimal(18, 0)");
        entity.Property(e => e.CostPrice)
            .HasDefaultValue(0m, "DF_StockVariance_CostPrice")
            .HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_StockVariance_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_StockVariance_IsActive");
        entity.Property(e => e.SalePrice)
            .HasDefaultValue(0m, "DF_StockVariance_SalePrice")
            .HasColumnType("decimal(18, 2)");
        entity.Property(e => e.StockCode).HasMaxLength(8);
        entity.Property(e => e.StockTypeCode).HasMaxLength(7);
        entity.Property(e => e.StockVarianceQuantity)
            .HasDefaultValue(0m, "DF_StockVariance_StockVarianceQuantity")
            .HasColumnType("decimal(18, 0)");

        entity.HasOne(d => d.StockCodeNavigation).WithMany(p => p.StockVariances)
            .HasPrincipalKey(p => p.StockCode)
            .HasForeignKey(d => d.StockCode)
            .HasConstraintName("FK_StockVariance_Stock");
    }
}
