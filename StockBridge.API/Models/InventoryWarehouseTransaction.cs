using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class InventoryWarehouseTransaction
{
    public long InventoryWarehouseTransactionId { get; set; }

    public string InventoryWarehouseType { get; set; } = null!;

    public double? InventoryWarehouseUno { get; set; }

    public float InventoryWarehouseLineNumber { get; set; }

    public int TerminalNumber { get; set; }

    public string? InventoryWarehouseStockP { get; set; }

    public string? InventoryWarehouseDescriptioncription { get; set; }

    public decimal? InventoryWarehouseQuantity { get; set; }

    public decimal? InventoryWarehouseQuantity00 { get; set; }

    public decimal? InventoryWarehouseQuantity01 { get; set; }

    public decimal? InventoryWarehouseQuantity02 { get; set; }

    public decimal? InventoryWarehouseQuantity03 { get; set; }

    public decimal? InventoryWarehouseQuantity04 { get; set; }

    public decimal? InventoryWarehouseQuantity05 { get; set; }

    public decimal? InventoryWarehouseQuantity06 { get; set; }

    public decimal? InventoryWarehouseQuantity07 { get; set; }

    public decimal? InventoryWarehouseQuantity08 { get; set; }

    public decimal? InventoryWarehouseQuantity09 { get; set; }

    public decimal? InventoryWarehouseQuantity10 { get; set; }

    public decimal? InventoryWarehouseQuantity11 { get; set; }

    public decimal? InventoryWarehouseQuantity12 { get; set; }

    public decimal? InventoryWarehouseQuantity13 { get; set; }

    public decimal? InventoryWarehouseQuantity14 { get; set; }

    public decimal? InventoryWarehouseSellingPrice { get; set; }

    public decimal? InventoryWarehouseCostPrice { get; set; }

    public decimal? InventoryWarehouseSdr { get; set; }

    public short? InventoryWarehouseSizeNumber { get; set; }

    public short? InventoryWarehouseSize00 { get; set; }

    public short? InventoryWarehouseSize01 { get; set; }

    public short? InventoryWarehouseSize02 { get; set; }

    public short? InventoryWarehouseSize03 { get; set; }

    public short? InventoryWarehouseSize04 { get; set; }

    public short? InventoryWarehouseSize05 { get; set; }

    public short? InventoryWarehouseSize06 { get; set; }

    public short? InventoryWarehouseSize07 { get; set; }

    public short? InventoryWarehouseSize08 { get; set; }

    public short? InventoryWarehouseSize09 { get; set; }

    public short? InventoryWarehouseSize10 { get; set; }

    public short? InventoryWarehouseSize11 { get; set; }

    public short? InventoryWarehouseSize12 { get; set; }

    public short? InventoryWarehouseSize13 { get; set; }

    public short? InventoryWarehouseSize14 { get; set; }

    public decimal? InventoryWarehouseDiscountPc { get; set; }

    public decimal? InventoryWarehouseNetAmount { get; set; }

    public decimal? InventoryWarehouseGrossAmount { get; set; }

    public decimal? InventoryWarehouseDiscountAmount { get; set; }

    public string? InventoryWarehouseSaveflg { get; set; }

    public decimal? ItemDiscountAmount { get; set; }

    public decimal? SpecialDiscountAmount { get; set; }

    public decimal? CashDiscountAmount { get; set; }

    public decimal? ItemDiscountRate { get; set; }

    public decimal? SpecialDiscountRate { get; set; }

    public decimal? CashDiscountRate { get; set; }

    public decimal? InventoryWarehouseAmountdiscount { get; set; }

    public decimal? InventoryWarehouseValueUediscount { get; set; }

    public string? InventoryWarehouseSalesRepresentativeresentativeCode { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Size? InventoryWarehouseSizeNumberNavigation { get; set; }

    public virtual Item? InventoryWarehouseStockPNavigation { get; set; }

    public virtual ICollection<InventoryWarehouseTransactionReturn> InventoryWarehouseTransactionReturns { get; set; } = new List<InventoryWarehouseTransactionReturn>();
}
