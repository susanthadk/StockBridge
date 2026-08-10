using System;

namespace StockBridge.Application.DTOs.Items;

public class ItemDto
{
    public int ItemId { get; set; }

    public string ItemCode { get; set; } = string.Empty;

    public string? ItemDescription { get; set; }

    public string CategoryCode { get; set; } = string.Empty;

    public string? FamilyCode { get; set; }

    public string? DepartmentCode { get; set; }

    public string? SupplierCode { get; set; }

    public short SizeNumber { get; set; }

    public double? FirstCost { get; set; }

    public double? TradeCost { get; set; }

    public decimal? CostPrice { get; set; }

    public decimal? SellingPrice { get; set; }

    public string? ItemMetadata { get; set; }

    public string? StockOnHandText { get; set; }

    public decimal? SalesDiscountRate { get; set; }

    public string? CustomerNumber { get; set; }

    public string? ActiveFlag { get; set; }

    public string? OpeningQuantity { get; set; }

    public string? GoodsReceiptQuantity { get; set; }

    public string? ReturnQuantity { get; set; }

    public string? SalesReturnQuantity { get; set; }

    public string? IssueQuantity { get; set; }

    public string? SalesQuantity { get; set; }

    public string? SupplierReturnQuantity { get; set; }

    public string? ClosingQuantity { get; set; }

    public string? ExecutedQuantity { get; set; }

    public string? ShortQuantity { get; set; }

    public string? Ifscode { get; set; }

    public string? AddedByUserCode { get; set; }

    public DateTime? AddedDate { get; set; }

    public string? AddedTime { get; set; }

    public string? AmendedByUserCode { get; set; }

    public string? AmendedDate { get; set; }

    public string? AmendedTime { get; set; }

    public string? AmendedTimeValue { get; set; }

    public string? EntryUserCode { get; set; }

    public string? EntryDate { get; set; }

    public string? EntryTime { get; set; }

    public string? PriceListFlag { get; set; }

    public string? SellingPrice1 { get; set; }

    public string? SellingPrice2 { get; set; }

    public string? RelatedItemCode { get; set; }

    public decimal? OrderNumber { get; set; }

    public string? ItemImage { get; set; }

    public string? AccountCode { get; set; }

    public string? DiscountFlag { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal? SalesCommission { get; set; }

    public string? MultiItemFlag { get; set; }

    public decimal? GrossProfit { get; set; }

    public decimal? ItemDiscount { get; set; }

    public decimal? SpecialDiscount { get; set; }

    public decimal? CashDiscount { get; set; }

    public string? GroupWithPriceFlag { get; set; }

    public string? SlowStockFlag { get; set; }

    public decimal? SlowStockCommission { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}