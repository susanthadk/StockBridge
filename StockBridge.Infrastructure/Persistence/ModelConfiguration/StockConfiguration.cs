using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> entity)
    {
        entity.ToTable("Stock");

        entity.HasIndex(e => e.StockCode, "UQ_Stock_BusinessKey").IsUnique();

        entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Stock_CreatedOn");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Stock_IsActive");
        entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.StockActiveFlag).HasMaxLength(1);
        entity.Property(e => e.StockAmendedByUserCode).HasMaxLength(20);
        entity.Property(e => e.StockAmendedDate).HasColumnType("datetime");
        entity.Property(e => e.StockAmendedTimee).HasMaxLength(12);
        entity.Property(e => e.StockCategory).HasMaxLength(4);
        entity.Property(e => e.StockCode).HasMaxLength(8);
        entity.Property(e => e.StockDepartment).HasMaxLength(10);
        entity.Property(e => e.StockDescriptioncription).HasMaxLength(40);
        entity.Property(e => e.StockDiscount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.StockDiscountEnabledFlag).HasMaxLength(1);
        entity.Property(e => e.StockEntryDate).HasColumnType("datetime");
        entity.Property(e => e.StockEntryTimee).HasMaxLength(12);
        entity.Property(e => e.StockEntryUserCode).HasMaxLength(20);
        entity.Property(e => e.StockExcessQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockFamily).HasMaxLength(10);
        entity.Property(e => e.StockFlag).HasMaxLength(1);
        entity.Property(e => e.StockFreeQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockFromDate).HasColumnType("datetime");
        entity.Property(e => e.StockGoodsReceiptQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockGroupWithPriceFlag).HasMaxLength(1);
        entity.Property(e => e.StockIfscode)
            .HasMaxLength(30)
            .HasColumnName("StockIFSCode");
        entity.Property(e => e.StockIssueQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockLtd).HasColumnType("datetime");
        entity.Property(e => e.StockMaximumQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockMinimumQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockMultiItemFlag).HasMaxLength(1);
        entity.Property(e => e.StockOnHand).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockOrderedQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockPurchaseQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockReservedQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockReturnQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockSalesAdjustmentQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockSalesQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockSalesReturnQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.StockSizeCode).HasMaxLength(6);
        entity.Property(e => e.StockSupplier).HasMaxLength(10);
        entity.Property(e => e.StockToDate).HasColumnType("datetime");
        entity.Property(e => e.StockTypeCode).HasMaxLength(7);
        entity.Property(e => e.StockVat).HasMaxLength(1);

        entity.HasOne(d => d.StockDepartmentNavigation).WithMany(p => p.Stocks)
            .HasPrincipalKey(p => p.DepartmentCode)
            .HasForeignKey(d => d.StockDepartment)
            .HasConstraintName("FK_Stock_Department");
    }
}
