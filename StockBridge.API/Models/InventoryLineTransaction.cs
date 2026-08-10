using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class InventoryLineTransaction
{
    public long InventoryLineTransactionId { get; set; }

    public string? InventoryLineLocation { get; set; }

    public string InventoryLineType { get; set; } = null!;

    public string InventoryLineDocumentNumber { get; set; } = null!;

    public int InventoryLineLineNumber { get; set; }

    public DateTime InventoryLineDate { get; set; }

    public string InventoryLineOperationCode { get; set; } = null!;

    public int TerminalNumber { get; set; }

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

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual InventoryHeaderTransaction InventoryHeaderTransaction { get; set; } = null!;

    public virtual Size? InventoryLineSizeNumberNavigation { get; set; }

    public virtual Item? InventoryLineStockPNavigation { get; set; }
}
