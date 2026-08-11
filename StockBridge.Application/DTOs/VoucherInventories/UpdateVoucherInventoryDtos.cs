using System.ComponentModel.DataAnnotations;

namespace StockBridge.Application.DTOs.VoucherInventories;

public class UpdateVoucherInventoryLineDto
{
    public long VoucherInventoryLineId { get; set; }

    [Required(ErrorMessage = "InventoryLineLineNumber is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "InventoryLineLineNumber must be a positive value.")]
    public int InventoryLineLineNumber { get; set; }

    public string? InventoryLineSmn { get; set; }

    public short? InventoryLineQuantity00 { get; set; }

    public short? InventoryLineQuantity01 { get; set; }

    public short? InventoryLineQuantity02 { get; set; }

    public short? InventoryLineQuantity03 { get; set; }

    public short? InventoryLineQuantity04 { get; set; }

    public short? InventoryLineQuantity05 { get; set; }

    public short? InventoryLineQuantity06 { get; set; }

    public short? InventoryLineQuantity07 { get; set; }

    public short? InventoryLineQuantity08 { get; set; }

    public short? InventoryLineQuantity09 { get; set; }

    public short? InventoryLineQuantity10 { get; set; }

    public short? InventoryLineQuantity11 { get; set; }

    public short? InventoryLineQuantity12 { get; set; }

    public short? InventoryLineQuantity13 { get; set; }

    public short? InventoryLineQuantity14 { get; set; }

    public int? InventoryLineQuantity { get; set; }

    public double? InventoryLineSellingPrice { get; set; }

    public double? InventoryLineCostPrice { get; set; }

    public string? InventoryLineStockP { get; set; }

    public string? InventoryLineVno { get; set; }

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

    public double? InventoryLineDiscountPc { get; set; }

    public double? InventoryLineNetAmount { get; set; }

    public double? InventoryLineGrossAmount { get; set; }

    public double? InventoryLineDiscountAmount { get; set; }

    public string? InventoryLineCustomer { get; set; }

    public string? InventoryLineSaveflg { get; set; }

    public string? InventoryLineReturn { get; set; }

    public string? InventoryLineAccount { get; set; }
}

public class UpdateVoucherInventoryHeaderDto
{
    [Required(ErrorMessage = "VoucherInventoryHeaderId is required.")]
    [Range(1, long.MaxValue, ErrorMessage = "VoucherInventoryHeaderId must be a positive value.")]
    public long VoucherInventoryHeaderId { get; set; }

    [Required(ErrorMessage = "InventoryHeaderLocation is required.")]
    [StringLength(6, ErrorMessage = "InventoryHeaderLocation cannot exceed 6 characters.")]
    public string InventoryHeaderLocation { get; set; } = null!;

    [Required(ErrorMessage = "InventoryHeaderType is required.")]
    [StringLength(2, ErrorMessage = "InventoryHeaderType cannot exceed 2 characters.")]
    public string InventoryHeaderType { get; set; } = null!;

    [Required(ErrorMessage = "InventoryHeaderDocumentNumber is required.")]
    [StringLength(14, ErrorMessage = "InventoryHeaderDocumentNumber cannot exceed 14 characters.")]
    public string InventoryHeaderDocumentNumber { get; set; } = null!;

    [Required(ErrorMessage = "InventoryHeaderDate is required.")]
    public DateTime InventoryHeaderDate { get; set; }

    [Required(ErrorMessage = "InventoryHeaderOperationCode is required.")]
    [StringLength(5, ErrorMessage = "InventoryHeaderOperationCode cannot exceed 5 characters.")]
    public string InventoryHeaderOperationCode { get; set; } = null!;

    [Required(ErrorMessage = "TerminalNumber is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "TerminalNumber must be a positive value.")]
    public int TerminalNumber { get; set; }

    public string? InventoryHeaderSupplier { get; set; }

    public string? InventoryHeaderReference { get; set; }

    public string? InventoryHeaderLocationTf { get; set; }

    public string? InventoryHeaderFrstk { get; set; }

    public string? InventoryHeaderTostk { get; set; }

    public string? InventoryHeaderCustomer { get; set; }

    public string? InventoryHeaderSaleTyp { get; set; }

    public string? InventoryHeaderPaytyp { get; set; }

    public int? InventoryHeaderQuantity { get; set; }

    public int? InventoryHeaderQuantity01 { get; set; }

    public int? InventoryHeaderQuantity02 { get; set; }

    public int? InventoryHeaderQuantity03 { get; set; }

    public int? InventoryHeaderQuantity04 { get; set; }

    public int? InventoryHeaderQuantity05 { get; set; }

    public int? InventoryHeaderQuantity06 { get; set; }

    public int? InventoryHeaderQuantity07 { get; set; }

    public int? InventoryHeaderQuantity08 { get; set; }

    public int? InventoryHeaderQuantity09 { get; set; }

    public int? InventoryHeaderQuantity10 { get; set; }

    public int? InventoryHeaderQuantity11 { get; set; }

    public int? InventoryHeaderQuantity12 { get; set; }

    public int? InventoryHeaderQuantity13 { get; set; }

    public int? InventoryHeaderQuantity14 { get; set; }

    public decimal? InventoryHeaderGrossAmount { get; set; }

    public decimal? InventoryHeaderDiscountAmount { get; set; }

    public decimal? InventoryHeaderNetAmount { get; set; }

    public decimal? InventoryHeaderCashDisamount { get; set; }

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

    public string? InventoryHeaderCrBnk { get; set; }

    public string? InventoryHeaderV01 { get; set; }

    public decimal? InventoryHeaderV01amount { get; set; }

    public string? InventoryHeaderV02 { get; set; }

    public decimal? InventoryHeaderV02amount { get; set; }

    public string? InventoryHeaderV03 { get; set; }

    public decimal? InventoryHeaderV03amount { get; set; }

    public DateTime? InventoryHeaderStimee { get; set; }

    public DateTime? InventoryHeaderEtimee { get; set; }

    public decimal? InventoryHeaderCash { get; set; }

    public DateTime? InventoryHeaderCashHt { get; set; }

    public string? InventoryHeaderD01 { get; set; }

    public decimal? InventoryHeaderD1amt1 { get; set; }

    public string? InventoryHeaderD02 { get; set; }

    public decimal? InventoryHeaderD2amount { get; set; }

    public decimal? InventoryHeaderD2amt2 { get; set; }

    public decimal? InventoryHeaderPayAmount { get; set; }

    public decimal? InventoryHeaderRgrsAmount { get; set; }

    public decimal? InventoryHeaderReturnQuantityAmount { get; set; }

    public decimal? InventoryHeaderTotalVod { get; set; }

    public decimal? InventoryHeaderTotalVodAmount { get; set; }

    public decimal? InventoryHeaderVatAmount { get; set; }

    public decimal? InventoryHeaderExcAmount { get; set; }

    public decimal? InventoryHeaderReferenceAmount { get; set; }

    public decimal? InventoryHeaderNetVatAmount { get; set; }

    public decimal? InventoryHeaderTotalQuantity { get; set; }

    public decimal? InventoryHeaderCreditDis { get; set; }

    public string? InventoryHeaderRebVou01 { get; set; }

    public string? InventoryHeaderRebVou02 { get; set; }

    public string? InventoryHeaderRebVou03 { get; set; }

    public string? InventoryHeaderVouCat01 { get; set; }

    public string? InventoryHeaderVouCat02 { get; set; }

    public string? InventoryHeaderVouCat03 { get; set; }

    public string? InventoryHeaderCrAccNo { get; set; }

    public string? InventoryHeaderCreditflg { get; set; }

    public List<UpdateVoucherInventoryLineDto> VoucherInventoryLines { get; set; } = new List<UpdateVoucherInventoryLineDto>();
}