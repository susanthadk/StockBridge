using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> entity)
    {
        entity.ToTable("Item");

        entity.HasIndex(e => e.ItemCode, "UQ_Item_BusinessKey").IsUnique();

        entity.Property(e => e.AccountCode).HasMaxLength(10);
        entity.Property(e => e.ActiveFlag).HasMaxLength(30);
        entity.Property(e => e.AddedByUserCode).HasMaxLength(30);
        entity.Property(e => e.AddedDate).HasColumnType("datetime");
        entity.Property(e => e.AddedTime).HasMaxLength(30);
        entity.Property(e => e.AmendedByUserCode).HasMaxLength(30);
        entity.Property(e => e.AmendedDate).HasMaxLength(30);
        entity.Property(e => e.AmendedTime).HasMaxLength(30);
        entity.Property(e => e.AmendedTimeValue).HasMaxLength(30);
        entity.Property(e => e.CashDiscount).HasColumnType("decimal(18, 4)");
        entity.Property(e => e.CategoryCode).HasMaxLength(10);
        entity.Property(e => e.ClosingQuantity).HasMaxLength(30);
        entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_Item_CreatedOn");
        entity.Property(e => e.CustomerNumber).HasMaxLength(30);
        entity.Property(e => e.DepartmentCode).HasMaxLength(10);
        entity.Property(e => e.DiscountFlag).HasMaxLength(1);
        entity.Property(e => e.EntryDate).HasMaxLength(30);
        entity.Property(e => e.EntryTime).HasMaxLength(30);
        entity.Property(e => e.EntryUserCode).HasMaxLength(30);
        entity.Property(e => e.ExecutedQuantity).HasMaxLength(30);
        entity.Property(e => e.FamilyCode).HasMaxLength(10);
        entity.Property(e => e.FromDate).HasColumnType("datetime");
        entity.Property(e => e.GoodsReceiptQuantity).HasMaxLength(30);
        entity.Property(e => e.GrossProfit).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.GroupWithPriceFlag).HasMaxLength(1);
        entity.Property(e => e.Ifscode)
            .HasMaxLength(30)
            .HasColumnName("IFSCode");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Item_IsActive");
        entity.Property(e => e.IssueQuantity).HasMaxLength(30);
        entity.Property(e => e.ItemCode).HasMaxLength(7);
        entity.Property(e => e.ItemDescription).HasMaxLength(40);
        entity.Property(e => e.ItemDiscount).HasColumnType("decimal(18, 4)");
        entity.Property(e => e.ItemImage).HasMaxLength(50);
        entity.Property(e => e.ItemMetadata).HasMaxLength(30);
        entity.Property(e => e.MultiItemFlag).HasMaxLength(1);
        entity.Property(e => e.OpeningQuantity).HasMaxLength(30);
        entity.Property(e => e.OrderNumber).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.PriceListFlag).HasMaxLength(30);
        entity.Property(e => e.RelatedItemCode).HasMaxLength(30);
        entity.Property(e => e.ReturnQuantity).HasMaxLength(30);
        entity.Property(e => e.SalesCommission)
            .HasDefaultValue(0m, "DF_Item_SalesCommission")
            .HasColumnType("decimal(18, 3)");
        entity.Property(e => e.SalesDiscountRate).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.SalesQuantity).HasMaxLength(30);
        entity.Property(e => e.SalesReturnQuantity).HasMaxLength(30);
        entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.SellingPrice1).HasMaxLength(30);
        entity.Property(e => e.SellingPrice2).HasMaxLength(30);
        entity.Property(e => e.ShortQuantity).HasMaxLength(30);
        entity.Property(e => e.SlowStockCommission).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.SlowStockFlag).HasMaxLength(1);
        entity.Property(e => e.SpecialDiscount).HasColumnType("decimal(18, 4)");
        entity.Property(e => e.StockOnHandText).HasMaxLength(30);
        entity.Property(e => e.SupplierCode).HasMaxLength(10);
        entity.Property(e => e.SupplierReturnQuantity).HasMaxLength(30);
        entity.Property(e => e.ToDate).HasColumnType("datetime");

        entity.HasOne(d => d.DepartmentCodeNavigation).WithMany(p => p.Items)
            .HasPrincipalKey(p => p.DepartmentCode)
            .HasForeignKey(d => d.DepartmentCode)
            .HasConstraintName("FK_Item_Department");

        entity.HasOne(d => d.SizeNumberNavigation).WithMany(p => p.Items)
            .HasPrincipalKey(p => p.SizeNumber)
            .HasForeignKey(d => d.SizeNumber)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Item_Size");

        entity.HasOne(d => d.SupplierCodeNavigation).WithMany(p => p.Items)
            .HasPrincipalKey(p => p.SupplierCode)
            .HasForeignKey(d => d.SupplierCode)
            .HasConstraintName("FK_Item_Supplier");

        entity.HasOne(d => d.Category).WithMany(p => p.Items)
            .HasPrincipalKey(p => new { p.CategoryCode, p.DepartmentCode })
            .HasForeignKey(d => new { d.CategoryCode, d.DepartmentCode })
            .HasConstraintName("FK_Item_Category");

        entity.HasOne(d => d.Family).WithMany(p => p.Items)
            .HasPrincipalKey(p => new { p.FamilyCode, p.CategoryCode, p.DepartmentCode })
            .HasForeignKey(d => new { d.FamilyCode, d.CategoryCode, d.DepartmentCode })
            .HasConstraintName("FK_Item_Family");
    }
}
