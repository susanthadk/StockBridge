using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class InventoryWarehouseTransactionReturnConfiguration : IEntityTypeConfiguration<InventoryWarehouseTransactionReturn>
{
    public void Configure(EntityTypeBuilder<InventoryWarehouseTransactionReturn> entity)
    {
        entity.ToTable("InventoryWarehouseTransactionReturn");

        entity.HasIndex(e => new { e.InventoryWarehouseDocumentNumber, e.InventoryWarehouseType, e.InventoryWarehouseLineNumber, e.TerminalNumber }, "UQ_InventoryWarehouseTransactionReturn_BusinessKey").IsUnique();

        entity.Property(e => e.CashDiscountAmount)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_CashDiscountAmount")
            .HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CashDiscountRate)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_CashDiscountRate")
            .HasColumnType("decimal(18, 4)");
        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_InventoryWarehouseTransactionReturn_CreatedOn");
        entity.Property(e => e.InventoryWarehouseAmountdiscount)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("InventoryWarehouseAMOUNTDISCOUNT");
        entity.Property(e => e.InventoryWarehouseCostPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryWarehouseDescriptioncription).HasMaxLength(40);
        entity.Property(e => e.InventoryWarehouseDiscountAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryWarehouseDiscountPc)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("InventoryWarehouseDiscountPC");
        entity.Property(e => e.InventoryWarehouseDocumentNumber).HasMaxLength(15);
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
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_InventoryWarehouseTransactionReturn_IsActive");
        entity.Property(e => e.ItemDiscountAmount)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_ItemDiscountAmount")
            .HasColumnType("decimal(18, 2)");
        entity.Property(e => e.ItemDiscountRate)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_ItemDiscountRate")
            .HasColumnType("decimal(18, 4)");
        entity.Property(e => e.SpecialDiscountAmount)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_SpecialDiscountAmount")
            .HasColumnType("decimal(18, 2)");
        entity.Property(e => e.SpecialDiscountRate)
            .HasDefaultValue(0m, "DF_InventoryWarehouseTransactionReturn_SpecialDiscountRate")
            .HasColumnType("decimal(18, 4)");

        entity.HasOne(d => d.InventoryWarehouseSizeNumberNavigation).WithMany(p => p.InventoryWarehouseTransactionReturns)
            .HasPrincipalKey(p => p.SizeNumber)
            .HasForeignKey(d => d.InventoryWarehouseSizeNumber)
            .HasConstraintName("FK_InventoryWarehouseTransactionReturn_Size");

        entity.HasOne(d => d.InventoryWarehouseStockPNavigation).WithMany(p => p.InventoryWarehouseTransactionReturns)
            .HasPrincipalKey(p => p.ItemCode)
            .HasForeignKey(d => d.InventoryWarehouseStockP)
            .HasConstraintName("FK_InventoryWarehouseTransactionReturn_Item");

        entity.HasOne(d => d.InventoryWarehouseTransaction).WithMany(p => p.InventoryWarehouseTransactionReturns)
            .HasPrincipalKey(p => new { p.InventoryWarehouseType, p.InventoryWarehouseLineNumber, p.TerminalNumber })
            .HasForeignKey(d => new { d.InventoryWarehouseType, d.InventoryWarehouseLineNumber, d.TerminalNumber })
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_InventoryWarehouseTransactionReturn_Transaction");
    }
}
