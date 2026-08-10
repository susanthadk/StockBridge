using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class StockOnHandSummary
{
    public int StockOnHandSummaryId { get; set; }

    public string ItemNumber { get; set; } = null!;

    public double? StockOnHand { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
