using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class StockDetail
{
    public long StockDetailId { get; set; }

    public DateTime? OpDate { get; set; }

    public DateTime? EnDate { get; set; }

    public decimal? PhyQuantity { get; set; }

    public decimal? PhyValue { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
