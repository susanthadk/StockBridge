using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class AreaRoute
{
    public int AreaRouteId { get; set; }

    public string AreaCode { get; set; } = null!;

    public string? AreaName { get; set; }

    public string? ShortName { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
    
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
