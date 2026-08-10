using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class SalesRepresentativeStockOnHand
{
    public long SalesRepresentativeStockOnHandId { get; set; }

    public string SalesRepresentativeresentativeCode { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public string StockCode { get; set; } = null!;

    public string? StockSizeCode { get; set; }

    public decimal? ItemCostPrice { get; set; }

    public decimal? ItemSellingPrice { get; set; }

    public decimal? StockOnHand { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Item ItemCodeNavigation { get; set; } = null!;

    public virtual SalesRepresentativeMaster SalesRepresentativeresentativeCodeNavigation { get; set; } = null!;

    public virtual Stock StockCodeNavigation { get; set; } = null!;
}
