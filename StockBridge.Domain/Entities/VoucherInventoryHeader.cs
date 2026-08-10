using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class VoucherInventoryHeader
{
    public long VoucherInventoryHeaderId { get; set; }

    public string InventoryHeaderLocation { get; set; } = null!;

    public string InventoryHeaderType { get; set; } = null!;

    public string InventoryHeaderDocumentNumber { get; set; } = null!;

    public DateTime InventoryHeaderDate { get; set; }

    public string InventoryHeaderOperationCode { get; set; } = null!;

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

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<VoucherInventoryLine> VoucherInventoryLines { get; set; } = new List<VoucherInventoryLine>();
}
