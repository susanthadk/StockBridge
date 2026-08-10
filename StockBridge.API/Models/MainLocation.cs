using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class MainLocation
{
    public int MainLocationId { get; set; }

    public string MainLocCode { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string LocType { get; set; } = null!;

    public string? LocationActiveFlag { get; set; }

    public bool? LinkedToCpu { get; set; }

    public string? DatabaseServer { get; set; }

    public string? DatabaseName { get; set; }

    public string? DatabaseUser { get; set; }

    public string? DatabasePassword { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<DocumentNumber> DocumentNumbers { get; set; } = new List<DocumentNumber>();
}
