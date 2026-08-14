using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class ItemType
{
    public int ItemTypeId { get; set; }

    public string ItemTypeCode { get; set; } = null!;

    public string ItemTypeName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsInventoryItem { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
