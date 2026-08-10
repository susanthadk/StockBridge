using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class InventoryWarehouseTransactionConfiguration : IEntityTypeConfiguration<InventoryWarehouseTransaction>
{
    public void Configure(EntityTypeBuilder<InventoryWarehouseTransaction> entity)
    {
        entity.ToTable("InventoryWarehouseTransaction");

        entity.HasIndex(e => new { e.InventoryWarehouseType, e.InventoryWarehouseLineNumber, e.TerminalNumber }, "UQ_InventoryWarehouseTransaction_BusinessKey").IsUnique();

        entity.Property(e => e.CashDiscountAmount)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_CashDiscountAmount")
            .HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CashDiscountRate)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_CashDiscountRate")
            .HasColumnType("decimal(18, 4)");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_InventoryWarehouseTransaction_CreatedOn");
        entity.Property(e => e.InventoryWarehouseAmountdiscount)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("InventoryWarehouseAMOUNTDISCOUNT");
        entity.Property(e => e.InventoryWarehouseCostPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryWarehouseDescriptioncription).HasMaxLength(40);
        entity.Property(e => e.InventoryWarehouseDiscountAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryWarehouseDiscountPc)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("InventoryWarehouseDiscountPC");
        entity.Property(e => e.InventoryWarehouseGrossAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryWarehouseNetAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryWarehouseQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity00).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity01).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity02).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity03).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity04).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity05).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity06).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity07).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity08).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity09).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity10).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity11).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity12).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity13).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseQuantity14).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryWarehouseSalesRepresentativeresentativeCode)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnName("InventoryWarehouseSalesRepresentativeresentativeCODE");
        entity.Property(e => e.InventoryWarehouseSaveflg)
            .HasMaxLength(1)
            .HasColumnName("InventoryWarehouseSAVEFLG");
        entity.Property(e => e.InventoryWarehouseSdr)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("InventoryWarehouseSDR");
        entity.Property(e => e.InventoryWarehouseSellingPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryWarehouseStockP).HasMaxLength(7);
        entity.Property(e => e.InventoryWarehouseType).HasMaxLength(2);
        entity.Property(e => e.InventoryWarehouseUno).HasColumnName("InventoryWarehouseUNO");
        entity.Property(e => e.InventoryWarehouseValueUediscount)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("InventoryWarehouseValueUEDISCOUNT");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_InventoryWarehouseTransaction_IsActive");
        entity.Property(e => e.ItemDiscountAmount)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_ItemDiscountAmount")
            .HasColumnType("decimal(18, 2)");
        entity.Property(e => e.ItemDiscountRate)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_ItemDiscountRate")
            .HasColumnType("decimal(18, 4)");
        entity.Property(e => e.SpecialDiscountAmount)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_SpecialDiscountAmount")
            .HasColumnType("decimal(18, 2)");
        entity.Property(e => e.SpecialDiscountRate)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransaction_SpecialDiscountRate")
            .HasColumnType("decimal(18, 4)");

        entity.HasOne(d => d.InventoryWarehouseSizeNumberNavigation).WithMany(p => p.InventoryWarehouseTransactions)
            .HasPrincipalKey(p => p.SizeNumber)
            .HasForeignKey(d => d.InventoryWarehouseSizeNumber)
            .HasConstraintName("FK_InventoryWarehouseTransaction_Size");

        entity.HasOne(d => d.InventoryWarehouseStockPNavigation).WithMany(p => p.InventoryWarehouseTransactions)
            .HasPrincipalKey(p => p.ItemCode)
            .HasForeignKey(d => d.InventoryWarehouseStockP)
            .HasConstraintName("FK_InventoryWarehouseTransaction_Item");
    }
}
