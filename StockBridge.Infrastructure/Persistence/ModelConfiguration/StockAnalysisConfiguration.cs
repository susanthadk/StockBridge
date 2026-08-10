using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class StockAnalysisConfiguration : IEntityTypeConfiguration<StockAnalysis>
{
    public void Configure(EntityTypeBuilder<StockAnalysis> entity)
    {
        entity.ToTable("StockAnalysis");

        entity.HasIndex(e => e.ItemType, "UQ_StockAnalysis_BusinessKey").IsUnique();

        entity.Property(e => e.CloseStk).HasColumnType("numeric(10, 0)");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_StockAnalysis_CreatedOn");
        entity.Property(e => e.Gin).HasColumnType("numeric(10, 0)");
        entity.Property(e => e.GoodsInNoteAsDate).HasColumnType("numeric(10, 0)");
        entity.Property(e => e.GoodsReceiptAsDate).HasColumnType("numeric(10, 0)");
        entity.Property(e => e.Grn).HasColumnType("numeric(10, 0)");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_StockAnalysis_IsActive");
        entity.Property(e => e.ItemType).HasMaxLength(13);
        entity.Property(e => e.OpenStk).HasColumnType("numeric(10, 0)");
        entity.Property(e => e.RetAsDate).HasColumnType("numeric(10, 0)");
        entity.Property(e => e.SaleEsAsDate).HasColumnType("numeric(10, 0)");
        entity.Property(e => e.SaleReturn).HasColumnType("numeric(10, 0)");
        entity.Property(e => e.Sales).HasColumnType("numeric(10, 0)");
        entity.Property(e => e.StockAsDate).HasColumnType("numeric(10, 0)");
    }
}
