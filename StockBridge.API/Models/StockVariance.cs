using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class StockVariance
{
    public long StockVarianceId { get; set; }

    public string StockCode { get; set; } = null!;

    public string? StockTypeCode { get; set; }

    public DateTime AdjDate { get; set; }

    public decimal? BeforeStockAdjustment { get; set; }

    public decimal? AfterStockAdjustment { get; set; }

    public decimal? StockVarianceQuantity { get; set; }

    public decimal? SalePrice { get; set; }

    public decimal? CostPrice { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Stock StockCodeNavigation { get; set; } = null!;
}
