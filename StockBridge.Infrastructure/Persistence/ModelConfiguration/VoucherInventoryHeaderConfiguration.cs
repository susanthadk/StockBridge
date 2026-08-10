using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class VoucherInventoryHeaderConfiguration : IEntityTypeConfiguration<VoucherInventoryHeader>
{
    public void Configure(EntityTypeBuilder<VoucherInventoryHeader> entity)
    {
        entity.ToTable("VoucherInventoryHeader");

        entity.HasIndex(e => new { e.InventoryHeaderLocation, e.InventoryHeaderType, e.InventoryHeaderDocumentNumber, e.InventoryHeaderDate, e.InventoryHeaderOperationCode, e.TerminalNumber }, "UQ_VoucherInventoryHeader_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_VoucherInventoryHeader_CreatedOn");
        entity.Property(e => e.CreditAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.CreditReceivedAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderAddedByUserCode).HasMaxLength(6);
        entity.Property(e => e.InventoryHeaderAddedDate).HasColumnType("datetime");
        entity.Property(e => e.InventoryHeaderAmendedByUserCode).HasMaxLength(6);
        entity.Property(e => e.InventoryHeaderAmendedDate).HasColumnType("datetime");
        entity.Property(e => e.InventoryHeaderCash).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderCashDisamount)
            .HasColumnType("decimal(18, 0)")
            .HasColumnName("InventoryHeaderCashDISAmount");
        entity.Property(e => e.InventoryHeaderCashDiscountPc)
            .HasColumnType("decimal(18, 0)")
            .HasColumnName("InventoryHeaderCashDiscountPC");
        entity.Property(e => e.InventoryHeaderCashHt).HasColumnType("datetime");
        entity.Property(e => e.InventoryHeaderCrAccNo).HasMaxLength(20);
        entity.Property(e => e.InventoryHeaderCrBnk).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderCreditBilno)
            .HasMaxLength(8)
            .HasColumnName("InventoryHeaderCreditBILNO");
        entity.Property(e => e.InventoryHeaderCreditDis).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderCreditNumber).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderCreditflg).HasMaxLength(1);
        entity.Property(e => e.InventoryHeaderCustomer).HasMaxLength(6);
        entity.Property(e => e.InventoryHeaderCustomerNam)
            .HasMaxLength(40)
            .HasColumnName("InventoryHeaderCustomerNAM");
        entity.Property(e => e.InventoryHeaderD01).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderD02).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderD1amt1)
            .HasColumnType("decimal(18, 0)")
            .HasColumnName("InventoryHeaderD1Amt1");
        entity.Property(e => e.InventoryHeaderD2amount)
            .HasColumnType("decimal(18, 0)")
            .HasColumnName("InventoryHeaderD2Amount");
        entity.Property(e => e.InventoryHeaderD2amt2)
            .HasColumnType("decimal(18, 0)")
            .HasColumnName("InventoryHeaderD2Amt2");
        entity.Property(e => e.InventoryHeaderDate).HasColumnType("datetime");
        entity.Property(e => e.InventoryHeaderDiscount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderDiscount1).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderDiscountAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderDiscountPc)
            .HasColumnType("decimal(18, 0)")
            .HasColumnName("InventoryHeaderDiscountPC");
        entity.Property(e => e.InventoryHeaderDocumentNumber).HasMaxLength(14);
        entity.Property(e => e.InventoryHeaderEtimee)
            .HasColumnType("datetime")
            .HasColumnName("InventoryHeaderETimee");
        entity.Property(e => e.InventoryHeaderExcAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderFrstk)
            .HasMaxLength(7)
            .HasColumnName("InventoryHeaderFRSTK");
        entity.Property(e => e.InventoryHeaderGrossAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderLocation).HasMaxLength(6);
        entity.Property(e => e.InventoryHeaderLocationTf)
            .HasMaxLength(6)
            .HasColumnName("InventoryHeaderLocationTF");
        entity.Property(e => e.InventoryHeaderNetAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderNetVatAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderNumberAme).HasColumnName("InventoryHeaderNumberAME");
        entity.Property(e => e.InventoryHeaderOperationCode).HasMaxLength(5);
        entity.Property(e => e.InventoryHeaderPayAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderPaytyp)
            .HasMaxLength(3)
            .HasColumnName("InventoryHeaderPAYTYP");
        entity.Property(e => e.InventoryHeaderPost)
            .HasMaxLength(1)
            .HasColumnName("InventoryHeaderPOST");
        entity.Property(e => e.InventoryHeaderPrintFlg)
            .HasMaxLength(1)
            .HasColumnName("InventoryHeaderPrintFLG");
        entity.Property(e => e.InventoryHeaderRebVou01).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderRebVou02).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderRebVou03).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderReference).HasMaxLength(20);
        entity.Property(e => e.InventoryHeaderReferenceAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderReturnQuantityAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderRgrsAmount)
            .HasColumnType("decimal(18, 0)")
            .HasColumnName("InventoryHeaderRGrsAmount");
        entity.Property(e => e.InventoryHeaderSaleTyp)
            .HasMaxLength(6)
            .HasColumnName("InventoryHeaderSaleTYP");
        entity.Property(e => e.InventoryHeaderStimee)
            .HasColumnType("datetime")
            .HasColumnName("InventoryHeaderSTimee");
        entity.Property(e => e.InventoryHeaderSupplier).HasMaxLength(6);
        entity.Property(e => e.InventoryHeaderTostk)
            .HasMaxLength(7)
            .HasColumnName("InventoryHeaderTOSTK");
        entity.Property(e => e.InventoryHeaderTotalQuantity).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderTotalVod).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderTotalVodAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderType).HasMaxLength(2);
        entity.Property(e => e.InventoryHeaderV01).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderV01amount)
            .HasColumnType("decimal(18, 0)")
            .HasColumnName("InventoryHeaderV01Amount");
        entity.Property(e => e.InventoryHeaderV02).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderV02amount)
            .HasColumnType("decimal(18, 0)")
            .HasColumnName("InventoryHeaderV02Amount");
        entity.Property(e => e.InventoryHeaderV03).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderV03amount)
            .HasColumnType("decimal(18, 0)")
            .HasColumnName("InventoryHeaderV03Amount");
        entity.Property(e => e.InventoryHeaderVatAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderVouCat01).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderVouCat02).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderVouCat03).HasMaxLength(10);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_VoucherInventoryHeader_IsActive");
    }
}
