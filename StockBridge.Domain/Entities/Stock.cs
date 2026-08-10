using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Stock
{
    public int StockId { get; set; }

    public string StockCode { get; set; } = null!;

    public string StockTypeCode { get; set; } = null!;

    public string StockSizeCode { get; set; } = null!;

    public string StockCategory { get; set; } = null!;

    public string? StockFamily { get; set; }

    public string? StockDepartment { get; set; }

    public string? StockSupplier { get; set; }

    public string? StockDescriptioncription { get; set; }

    public DateTime? StockLtd { get; set; }

    public decimal? CostPrice { get; set; }

    public decimal? SellingPrice { get; set; }

    public int? StockOpeningStock { get; set; }

    public decimal? StockMinimumQuantity { get; set; }

    public decimal? StockMaximumQuantity { get; set; }

    public decimal? StockOnHand { get; set; }

    public decimal? StockReservedQuantity { get; set; }

    public decimal? StockOrderedQuantity { get; set; }

    public decimal? StockPurchaseQuantity { get; set; }

    public decimal? StockReturnQuantity { get; set; }

    public decimal? StockIssueQuantity { get; set; }

    public decimal? StockFreeQuantity { get; set; }

    public decimal? StockGoodsReceiptQuantity { get; set; }

    public decimal? StockExcessQuantity { get; set; }

    public decimal? StockSalesQuantity { get; set; }

    public decimal? StockSalesAdjustmentQuantity { get; set; }

    public decimal? StockSalesReturnQuantity { get; set; }

    public string? StockFlag { get; set; }

    public string? StockVat { get; set; }

    public decimal? StockDiscount { get; set; }

    public string? StockEntryUserCode { get; set; }

    public DateTime? StockEntryDate { get; set; }

    public string? StockEntryTimee { get; set; }

    public string? StockAmendedByUserCode { get; set; }

    public DateTime? StockAmendedDate { get; set; }

    public string? StockAmendedTimee { get; set; }

    public string? StockActiveFlag { get; set; }

    public string? StockIfscode { get; set; }

    public DateTime? StockFromDate { get; set; }

    public DateTime? StockToDate { get; set; }

    public string? StockDiscountEnabledFlag { get; set; }

    public string? StockMultiItemFlag { get; set; }

    public string? StockGroupWithPriceFlag { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<MultiItem> MultiItems { get; set; } = new List<MultiItem>();

    public virtual ICollection<SalesRepresentativeStockOnHand> SalesRepresentativeStockOnHands { get; set; } = new List<SalesRepresentativeStockOnHand>();

    public virtual Department? StockDepartmentNavigation { get; set; }

    public virtual Supplier? StockSupplierNavigation { get; set; }

    public virtual Item StockTypeCodeNavigation { get; set; } = null!;

    public virtual ICollection<StockVariance> StockVariances { get; set; } = new List<StockVariance>();

    public virtual ICollection<StoreTransferTransaction> StoreTransferTransactions { get; set; } = new List<StoreTransferTransaction>();
}
