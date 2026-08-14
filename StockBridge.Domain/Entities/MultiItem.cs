using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class MultiItem
{
    public int MultiItemId { get; set; }

    public string StockCode { get; set; } = null!;

    public string StockTypeCode { get; set; } = null!;

    public string StockSizeCode { get; set; } = null!;

    public string? StockDescriptioncription { get; set; }

    public decimal? CostPrice { get; set; }

    public decimal SellingPrice { get; set; }

    public decimal? StockOnHand { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedByUser { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Stock StockCodeNavigation { get; set; } = null!;
}
