using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class SalesRepresentativeStockOnHandConfiguration : IEntityTypeConfiguration<SalesRepresentativeStockOnHand>
{
    public void Configure(EntityTypeBuilder<SalesRepresentativeStockOnHand> entity)
    {
        entity.ToTable("SalesRepresentativeStockOnHand");

        entity.HasIndex(e => new { e.SalesRepresentativeresentativeCode, e.StockCode }, "UQ_SalesRepresentativeStockOnHand_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_SalesRepresentativeStockOnHand_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_SalesRepresentativeStockOnHand_IsActive");
        entity.Property(e => e.ItemCode).HasMaxLength(7);
        entity.Property(e => e.ItemCostPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.ItemSellingPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.SalesRepresentativeresentativeCode).HasMaxLength(10);
        entity.Property(e => e.StockCode).HasMaxLength(8);
        entity.Property(e => e.StockOnHand).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockSizeCode).HasMaxLength(6);

        entity.HasOne(d => d.SalesRepresentativeresentativeCodeNavigation).WithMany(p => p.SalesRepresentativeStockOnHands)
            .HasPrincipalKey(p => p.SalesRepresentativeresentativeCode)
            .HasForeignKey(d => d.SalesRepresentativeresentativeCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SalesRepresentativeStockOnHand_SalesRepresentativeMaster");

        entity.HasOne(d => d.StockCodeNavigation).WithMany(p => p.SalesRepresentativeStockOnHands)
            .HasPrincipalKey(p => p.StockCode)
            .HasForeignKey(d => d.StockCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SalesRepresentativeStockOnHand_Stock");
    }
}
