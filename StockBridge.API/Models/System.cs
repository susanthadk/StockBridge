using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class System
{
    public int SystemId { get; set; }

    public double SystemRecordType { get; set; }

    public string SystemRecordNumber { get; set; } = null!;

    public string? SystemDescription { get; set; }

    public string? SystemDescription2 { get; set; }

    public decimal? SystemAmount { get; set; }

    public decimal? SystemAmount2 { get; set; }

    public DateTime? SystemFromDate { get; set; }

    public DateTime? SystemToDate { get; set; }

    public decimal? ValueAmount { get; set; }

    public decimal? ValueAmount2 { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
