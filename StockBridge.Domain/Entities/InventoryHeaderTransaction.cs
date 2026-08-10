using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class InventoryHeaderTransaction
{
    public long InventoryHeaderTransactionId { get; set; }

    public string? InventoryHeaderLocation { get; set; }

    public string InventoryHeaderType { get; set; } = null!;

    public string InventoryHeaderDocumentNumber { get; set; } = null!;

    public DateTime InventoryHeaderDate { get; set; }

    public string InventoryHeaderOperationCode { get; set; } = null!;

    public int TerminalNumber { get; set; }

    public string? InventoryHeaderSupplier { get; set; }

    public string? InventoryHeaderSrepresentative { get; set; }

    public string? InventoryHeaderReference { get; set; }

    public string? InventoryHeaderLocationTf { get; set; }

    public string? InventoryHeaderFrstk { get; set; }

    public string? InventoryHeaderTostk { get; set; }

    public string? InventoryHeaderCustomer { get; set; }

    public string? InventoryHeaderSaleTyp { get; set; }

    public string? InventoryHeaderPaytyp { get; set; }

    public decimal? InventoryHeaderQuantity { get; set; }

    public decimal? InventoryHeaderQuantity01 { get; set; }

    public decimal? InventoryHeaderQuantity02 { get; set; }

    public decimal? InventoryHeaderQuantity03 { get; set; }

    public decimal? InventoryHeaderQuantity04 { get; set; }

    public decimal? InventoryHeaderQuantity05 { get; set; }

    public decimal? InventoryHeaderQuantity06 { get; set; }

    public decimal? InventoryHeaderQuantity07 { get; set; }

    public decimal? InventoryHeaderQuantity08 { get; set; }

    public decimal? InventoryHeaderQuantity09 { get; set; }

    public decimal? InventoryHeaderQuantity10 { get; set; }

    public decimal? InventoryHeaderQuantity11 { get; set; }

    public decimal? InventoryHeaderQuantity12 { get; set; }

    public decimal? InventoryHeaderQuantity13 { get; set; }

    public decimal? InventoryHeaderQuantity14 { get; set; }

    public decimal? InventoryHeaderGrossAmount { get; set; }

    public decimal? InventoryHeaderDiscountAmount { get; set; }

    public decimal? InventoryHeaderNetAmount { get; set; }

    public decimal? InventoryHeaderCashDiscountAmount { get; set; }

    public decimal? InventoryHeaderDiscountPc { get; set; }

    public string? InventoryHeaderPost { get; set; }

    public decimal? InventoryHeaderCashDiscountPc { get; set; }

    public string? InventoryHeaderPrintFlg { get; set; }

    public int? InventoryHeaderNumberAme { get; set; }

    public string? InventoryHeaderAddedByUserCode { get; set; }

    public string? InventoryHeaderAmendedByUserCode { get; set; }

    public DateTime? InventoryHeaderAddedDate { get; set; }

    public DateTime? InventoryHeaderAmendedDate { get; set; }

    public string? InventoryHeaderCustomerNam { get; set; }

    public string? InventoryHeaderCreditBilno { get; set; }

    public decimal? CreditReceivedAmount { get; set; }

    public decimal? InventoryHeaderDiscount { get; set; }

    public decimal? InventoryHeaderDiscount1 { get; set; }

    public string? InventoryHeaderCreditNumber { get; set; }

    public decimal? CreditAmount { get; set; }

    public string? InventoryHeaderCreditBankCode { get; set; }

    public string? InventoryHeaderVoucher01Code { get; set; }

    public decimal? InventoryHeaderVoucher01Amount { get; set; }

    public string? InventoryHeaderVoucher02Code { get; set; }

    public decimal? InventoryHeaderVoucher02Amount { get; set; }

    public string? InventoryHeaderVoucher03Code { get; set; }

    public decimal? InventoryHeaderVoucher03Amount { get; set; }

    public DateTime? InventoryHeaderStimee { get; set; }

    public DateTime? InventoryHeaderEtimee { get; set; }

    public decimal? InventoryHeaderCashAmount { get; set; }

    public decimal? InventoryHeaderCashHandoverTime { get; set; }

    public string? InventoryHeaderDiscount01 { get; set; }

    public decimal? InventoryHeaderDiscount1Amount1 { get; set; }

    public string? InventoryHeaderDiscount02 { get; set; }

    public decimal? InventoryHeaderDiscount2Amount { get; set; }

    public decimal? InventoryHeaderDiscount2Amount2 { get; set; }

    public decimal? InventoryHeaderPaymentAmount { get; set; }

    public decimal? InventoryHeaderReturnGrossAmount { get; set; }

    public decimal? InventoryHeaderReturnQuantityAmount { get; set; }

    public decimal? InventoryHeaderTotalVoucher { get; set; }

    public decimal? InventoryHeaderTotalVoucherAmount { get; set; }

    public decimal? InventoryHeaderVatAmount { get; set; }

    public decimal? InventoryHeaderExciseAmount { get; set; }

    public decimal? InventoryHeaderReferenceAmount { get; set; }

    public decimal? InventoryHeaderNetVatAmount { get; set; }

    public decimal? InventoryHeaderTotalQuantity { get; set; }

    public decimal? InventoryHeaderCreditDiscount { get; set; }

    public string? InventoryHeaderRebateVoucher01 { get; set; }

    public string? InventoryHeaderRebateVoucher02 { get; set; }

    public string? InventoryHeaderRebateVoucher03 { get; set; }

    public string? InventoryHeaderVouCat01 { get; set; }

    public string? InventoryHeaderVouCat02 { get; set; }

    public string? InventoryHeaderVouCat03 { get; set; }

    public string? InventoryHeaderCreditAccountNumber { get; set; }

    public string? InventoryHeaderCreditFlag { get; set; }

    public string? InventoryHeaderEntryUserCode { get; set; }

    public decimal? InventoryHeaderChqAmount { get; set; }

    public string? InventoryHeaderCreditType { get; set; }

    public string? InventoryHeaderCreditName { get; set; }

    public decimal? TotalCreditAmount { get; set; }

    public string? InventoryHeaderReturnDocumentNumber { get; set; }

    public string? InventoryHeaderSalesRepresentativeresentativeCode { get; set; }

    public string? InventoryHeaderAreaCode { get; set; }

    public string? InventoryHeaderCompanyCode { get; set; }

    public decimal? InventoryHeaderCashDiscountcount { get; set; }

    public decimal? InventoryHeaderCashDiscountcountRate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<InventoryLineTransaction> InventoryLineTransactions { get; set; } = new List<InventoryLineTransaction>();
}
