using System.ComponentModel.DataAnnotations;

namespace StockBridge.Application.DTOs.InventoryTransactions;

public class CreateInventoryLineTransactionDto
{
    [Required(ErrorMessage = "InventoryLineLineNumber is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "InventoryLineLineNumber must be a positive value.")]
    public int InventoryLineLineNumber { get; set; }

    public string? InventoryLineLocation { get; set; }

    public string? InventoryLineSmn { get; set; }

    public decimal? InventoryLineQuantity00 { get; set; }

    public decimal? InventoryLineQuantity01 { get; set; }

    public decimal? InventoryLineQuantity02 { get; set; }

    public decimal? InventoryLineQuantity03 { get; set; }

    public decimal? InventoryLineQuantity04 { get; set; }

    public decimal? InventoryLineQuantity05 { get; set; }

    public decimal? InventoryLineQuantity06 { get; set; }

    public decimal? InventoryLineQuantity07 { get; set; }

    public decimal? InventoryLineQuantity08 { get; set; }

    public decimal? InventoryLineQuantity09 { get; set; }

    public decimal? InventoryLineQuantity10 { get; set; }

    public decimal? InventoryLineQuantity11 { get; set; }

    public decimal? InventoryLineQuantity12 { get; set; }

    public decimal? InventoryLineQuantity13 { get; set; }

    public decimal? InventoryLineQuantity14 { get; set; }

    public decimal? InventoryLineQuantity { get; set; }

    public decimal? InventoryLineSellingPrice { get; set; }

    public decimal? InventoryLineCostPrice { get; set; }

    public string? InventoryLineStockP { get; set; }

    public short? InventoryLineSizeNumber { get; set; }

    public string? InventoryLineDescriptioncription { get; set; }

    public short? InventoryLineSize00 { get; set; }

    public short? InventoryLineSize01 { get; set; }

    public short? InventoryLineSize02 { get; set; }

    public short? InventoryLineSize03 { get; set; }

    public short? InventoryLineSize04 { get; set; }

    public short? InventoryLineSize05 { get; set; }

    public short? InventoryLineSize06 { get; set; }

    public short? InventoryLineSize07 { get; set; }

    public short? InventoryLineSize08 { get; set; }

    public short? InventoryLineSize09 { get; set; }

    public short? InventoryLineSize10 { get; set; }

    public short? InventoryLineSize11 { get; set; }

    public short? InventoryLineSize12 { get; set; }

    public short? InventoryLineSize13 { get; set; }

    public short? InventoryLineSize14 { get; set; }

    public string? InventoryLineLocationTf { get; set; }

    public decimal? InventoryLineDiscountPc { get; set; }

    public decimal? InventoryLineNetAmount { get; set; }

    public decimal? InventoryLineGrossAmount { get; set; }

    public decimal? InventoryLineDiscountAmount { get; set; }

    public string? InventoryLineCustomer { get; set; }

    public string? InventoryLineSaveflg { get; set; }

    public string? InventoryLineReturn { get; set; }

    public string? InventoryLineAccount { get; set; }

    public string? InventoryLineCategory { get; set; }

    public string? InventoryLineEntryUserCode { get; set; }

    public decimal? InventoryLineItemDiscountAmount { get; set; }

    public decimal? InventoryLineSpecialDiscountAmount { get; set; }

    public decimal? InventoryLineCashDiscountAmount { get; set; }

    public decimal? InventoryLineItemDiscountRate { get; set; }

    public decimal? InventoryLineSpecialDiscountRate { get; set; }

    public decimal? InventoryLineCashDiscountRate { get; set; }

    public decimal? InventoryLineAmountdiscount { get; set; }

    public decimal? InventoryLineValueUediscount { get; set; }

    public string? InventoryLineSalesRepresentativeresentativeCode { get; set; }
}

public class CreateInventoryHeaderTransactionDto
{
    [Required(ErrorMessage = "InventoryHeaderType is required.")]
    [StringLength(2, ErrorMessage = "InventoryHeaderType cannot exceed 2 characters.")]
    public string InventoryHeaderType { get; set; } = null!;

    [Required(ErrorMessage = "InventoryHeaderDocumentNumber is required.")]
    [StringLength(15, ErrorMessage = "InventoryHeaderDocumentNumber cannot exceed 15 characters.")]
    public string InventoryHeaderDocumentNumber { get; set; } = null!;

    [Required(ErrorMessage = "InventoryHeaderDate is required.")]
    public DateTime InventoryHeaderDate { get; set; }

    [Required(ErrorMessage = "InventoryHeaderOperationCode is required.")]
    [StringLength(5, ErrorMessage = "InventoryHeaderOperationCode cannot exceed 5 characters.")]
    public string InventoryHeaderOperationCode { get; set; } = null!;

    [Required(ErrorMessage = "TerminalNumber is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "TerminalNumber must be a positive value.")]
    public int TerminalNumber { get; set; }

    public string? InventoryHeaderLocation { get; set; }

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

    [Required(ErrorMessage = "At least one line is required.")]
    [MinLength(1, ErrorMessage = "At least one line is required.")]
    public List<CreateInventoryLineTransactionDto> InventoryLineTransactions { get; set; } = new List<CreateInventoryLineTransactionDto>();
}