using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBridge.Domain.Entities;

namespace StockBridge.Infrastructure.Persistence.ModelConfiguration;

public class InventoryHeaderTransactionConfiguration : IEntityTypeConfiguration<InventoryHeaderTransaction>
{
    public void Configure(EntityTypeBuilder<InventoryHeaderTransaction> entity)
    {
        entity.ToTable("InventoryHeaderTransaction");

        entity.HasIndex(e => new { e.InventoryHeaderType, e.InventoryHeaderDocumentNumber, e.InventoryHeaderDate, e.InventoryHeaderOperationCode, e.TerminalNumber }, "UQ_InventoryHeaderTransaction_BusinessKey").IsUnique();

        entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysutcdatetime())", "DF_InventoryHeaderTransaction_CreatedOn");
        entity.Property(e => e.CreditAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.CreditReceivedAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderAddedByUserCode).HasMaxLength(6);
        entity.Property(e => e.InventoryHeaderAddedDate).HasColumnType("datetime");
        entity.Property(e => e.InventoryHeaderAmendedByUserCode).HasMaxLength(6);
        entity.Property(e => e.InventoryHeaderAmendedDate).HasColumnType("datetime");
        entity.Property(e => e.InventoryHeaderAreaCode)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.InventoryHeaderCashAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderCashDiscountAmount).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.InventoryHeaderCashDiscountPc)
            .HasColumnType("decimal(18, 3)")
            .HasColumnName("InventoryHeaderCashDiscountPC");
        entity.Property(e => e.InventoryHeaderCashDiscountcount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderCashDiscountcountRate).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.InventoryHeaderCashHandoverTime).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderChqAmount).HasColumnType("decimal(18, 0)");
        entity.Property(e => e.InventoryHeaderCompanyCode).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderCreditAccountNumber).HasMaxLength(20);
        entity.Property(e => e.InventoryHeaderCreditBankCode).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderCreditBilno)
            .HasMaxLength(8)
            .HasColumnName("InventoryHeaderCreditBILNO");
        entity.Property(e => e.InventoryHeaderCreditDiscount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderCreditFlag).HasMaxLength(1);
        entity.Property(e => e.InventoryHeaderCreditName)
            .HasMaxLength(20)
            .IsUnicode(false);
        entity.Property(e => e.InventoryHeaderCreditNumber).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderCreditType)
            .HasMaxLength(15)
            .IsUnicode(false);
        entity.Property(e => e.InventoryHeaderCustomer).HasMaxLength(6);
        entity.Property(e => e.InventoryHeaderCustomerNam)
            .HasMaxLength(40)
            .HasColumnName("InventoryHeaderCustomerNAM");
        entity.Property(e => e.InventoryHeaderDate).HasColumnType("datetime");
        entity.Property(e => e.InventoryHeaderDiscount).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.InventoryHeaderDiscount01).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderDiscount02).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderDiscount1).HasColumnType("decimal(18, 3)");
        entity.Property(e => e.InventoryHeaderDiscount1Amount1).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderDiscount2Amount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderDiscount2Amount2).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderDiscountAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderDiscountPc)
            .HasColumnType("decimal(18, 3)")
            .HasColumnName("InventoryHeaderDiscountPC");
        entity.Property(e => e.InventoryHeaderDocumentNumber).HasMaxLength(15);
        entity.Property(e => e.InventoryHeaderEntryUserCode).HasMaxLength(30);
        entity.Property(e => e.InventoryHeaderEtimee)
            .HasColumnType("datetime")
            .HasColumnName("InventoryHeaderETimee");
        entity.Property(e => e.InventoryHeaderExciseAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderFrstk)
            .HasMaxLength(7)
            .HasColumnName("InventoryHeaderFRSTK");
        entity.Property(e => e.InventoryHeaderGrossAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderLocation).HasMaxLength(6);
        entity.Property(e => e.InventoryHeaderLocationTf)
            .HasMaxLength(6)
            .HasColumnName("InventoryHeaderLocationTF");
        entity.Property(e => e.InventoryHeaderNetAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderNetVatAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderNumberAme).HasColumnName("InventoryHeaderNumberAME");
        entity.Property(e => e.InventoryHeaderOperationCode).HasMaxLength(5);
        entity.Property(e => e.InventoryHeaderPaymentAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderPaytyp)
            .HasMaxLength(3)
            .HasColumnName("InventoryHeaderPAYTYP");
        entity.Property(e => e.InventoryHeaderPost)
            .HasMaxLength(1)
            .HasColumnName("InventoryHeaderPOST");
        entity.Property(e => e.InventoryHeaderPrintFlg)
            .HasMaxLength(1)
            .HasColumnName("InventoryHeaderPrintFLG");
        entity.Property(e => e.InventoryHeaderQuantity).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity01).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity02).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity03).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity04).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity05).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity06).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity07).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity08).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity09).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity10).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity11).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity12).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity13).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderQuantity14).HasColumnType("decimal(10, 0)");
        entity.Property(e => e.InventoryHeaderRebateVoucher01).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderRebateVoucher02).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderRebateVoucher03).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderReference).HasMaxLength(20);
        entity.Property(e => e.InventoryHeaderReferenceAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderReturnDocumentNumber).HasMaxLength(15);
        entity.Property(e => e.InventoryHeaderReturnGrossAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderReturnQuantityAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderSaleTyp)
            .HasMaxLength(6)
            .HasColumnName("InventoryHeaderSaleTYP");
        entity.Property(e => e.InventoryHeaderSalesRepresentativeresentativeCode)
            .HasMaxLength(10)
            .IsUnicode(false);
        entity.Property(e => e.InventoryHeaderSrepresentative)
            .HasMaxLength(10)
            .HasColumnName("InventoryHeaderSRepresentative");
        entity.Property(e => e.InventoryHeaderStimee)
            .HasColumnType("datetime")
            .HasColumnName("InventoryHeaderSTimee");
        entity.Property(e => e.InventoryHeaderSupplier).HasMaxLength(6);
        entity.Property(e => e.InventoryHeaderTostk)
            .HasMaxLength(7)
            .HasColumnName("InventoryHeaderTOSTK");
        entity.Property(e => e.InventoryHeaderTotalQuantity).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderTotalVoucher).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderTotalVoucherAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderType).HasMaxLength(2);
        entity.Property(e => e.InventoryHeaderVatAmount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderVouCat01).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderVouCat02).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderVouCat03).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderVoucher01Amount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderVoucher01Code).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderVoucher02Amount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderVoucher02Code).HasMaxLength(10);
        entity.Property(e => e.InventoryHeaderVoucher03Amount).HasColumnType("decimal(18, 2)");
        entity.Property(e => e.InventoryHeaderVoucher03Code).HasMaxLength(10);
        entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_InventoryHeaderTransaction_IsActive");
        entity.Property(e => e.TotalCreditAmount).HasColumnType("decimal(18, 2)");
    }
}
