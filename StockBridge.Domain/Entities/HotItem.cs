using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class HotItem
{
    public int HotItemId { get; set; }

    public string? ItemCode { get; set; }

    public string? Descriptioncription { get; set; }

    public decimal? Quantity { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Item? ItemCodeNavigation { get; set; }
}
