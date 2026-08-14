using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class InventoryLineTransactionConfiguration : IEntityTypeConfiguration<InventoryLineTransaction>
{
    public void Configure(EntityTypeBuilder<InventoryLineTransaction> entity)
    {
        entity.ToTable("InventoryLineTransaction");

        entity.HasIndex(e => new { e.InventoryLineType, e.InventoryLineDocumentNumber, e.InventoryLineLineNumber, e.InventoryLineDate, e.InventoryLineOperationCode, e.TerminalNumber }, "UQ_InventoryLineTransaction_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_InventoryLineTransaction_CreatedOn");
        entity.Property(e => e.InventoryLineAccount).HasMaxLength(10);
        entity.Property(e => e.InventoryLineAmountdiscount)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("InventoryLineAMOUNTDISCOUNT");
        entity.Property(e => e.InventoryLineCashDiscountAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryLineCashDiscountRate)
            .HasColumnType("decimal(18, 4)")
            .HasColumnName("InventoryLineCashDiscountRATE");
        entity.Property(e => e.InventoryLineCategory).HasMaxLength(5);
        entity.Property(e => e.InventoryLineCostPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryLineCustomer).HasMaxLength(6);
        entity.Property(e => e.InventoryLineDate).HasColumnType("datetime");
        entity.Property(e => e.InventoryLineDescriptioncription).HasMaxLength(40);
        entity.Property(e => e.InventoryLineDiscountAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryLineDiscountPc)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("InventoryLineDiscountPC");
        entity.Property(e => e.InventoryLineDocumentNumber).HasMaxLength(15);
        entity.Property(e => e.InventoryLineEntryUserCode).HasMaxLength(30);
        entity.Property(e => e.InventoryLineGrossAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryLineItemDiscountAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryLineItemDiscountRate)
            .HasColumnType("decimal(18, 4)")
            .HasColumnName("InventoryLineItemDiscountRATE");
        entity.Property(e => e.InventoryLineLocation).HasMaxLength(6);
        entity.Property(e => e.InventoryLineLocationTf)
            .HasMaxLength(6)
            .HasColumnName("InventoryLineLocationTF");
        entity.Property(e => e.InventoryLineNetAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryLineOperationCode).HasMaxLength(5);
        entity.Property(e => e.InventoryLineQuantity).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity00).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity01).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity02).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity03).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity04).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity05).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity06).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity07).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity08).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity09).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity10).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity11).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity12).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity13).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineQuantity14).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryLineReturn).HasMaxLength(1);
        entity.Property(e => e.InventoryLineSalesRepresentativeresentativeCode)
            .HasMaxLength(10)
            .IsUnicode(false)
            .HasColumnName("InventoryLineSalesRepresentativeresentativeCODE");
        entity.Property(e => e.InventoryLineSaveflg)
            .HasMaxLength(1)
            .HasColumnName("InventoryLineSAVEFLG");
        entity.Property(e => e.InventoryLineSellingPrice).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryLineSmn)
            .HasMaxLength(10)
            .HasColumnName("InventoryLineSMN");
        entity.Property(e => e.InventoryLineSpecialDiscountAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryLineSpecialDiscountRate)
            .HasColumnType("decimal(18, 4)")
            .HasColumnName("InventoryLineSpecialDiscountRATE");
        entity.Property(e => e.InventoryLineStockP).HasMaxLength(7);
        entity.Property(e => e.InventoryLineType).HasMaxLength(2);
        entity.Property(e => e.InventoryLineValueUediscount)
            .HasColumnType("decimal(18, 2)")
            .HasColumnName("InventoryLineValueUEDISCOUNT");
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_InventoryLineTransaction_IsActive");

        entity.HasOne(d => d.InventoryLineSizeNumberNavigation).WithMany(p => p.InventoryLineTransactions)
            .HasPrincipalKey(p => p.SizeNumber)
            .HasForeignKey(d => d.InventoryLineSizeNumber)
            .HasConstraintName("FK_InventoryLineTransaction_Size");

        entity.HasOne(d => d.InventoryHeaderTransaction).WithMany(p => p.InventoryLineTransactions)
            .HasPrincipalKey(p => new { p.InventoryHeaderType, p.InventoryHeaderDocumentNumber, p.InventoryHeaderDate, p.InventoryHeaderOperationCode, p.TerminalNumber })
            .HasForeignKey(d => new { d.InventoryLineType, d.InventoryLineDocumentNumber, d.InventoryLineDate, d.InventoryLineOperationCode, d.TerminalNumber })
            .HasConstraintName("FK_InventoryLineTransaction_InventoryHeaderTransaction");
    }
}
