using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class VersionHeader
{
    public int VersionHeaderId { get; set; }

    public string? Version { get; set; }

    public double? Day { get; set; }

    public double? Month { get; set; }

    public double? Year { get; set; }

    public DateTime? Date { get; set; }

    public string? Sales { get; set; }

    public string? SalesReturn { get; set; }

    public string? Receipt { get; set; }

    public string? Stock { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
