using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class VoucherInventoryLine
{
    public long VoucherInventoryLineId { get; set; }

    public string InventoryLineLocation { get; set; } = null!;

    public string InventoryLineType { get; set; } = null!;

    public string InventoryLineDocumentNumber { get; set; } = null!;

    public int InventoryLineLineNumber { get; set; }

    public DateTime InventoryLineDate { get; set; }

    public string InventoryLineOperationCode { get; set; } = null!;

    public int TerminalNumber { get; set; }

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

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Size? InventoryLineSizeNumberNavigation { get; set; }

    public virtual VoucherInventoryHeader VoucherInventoryHeader { get; set; } = null!;
}
