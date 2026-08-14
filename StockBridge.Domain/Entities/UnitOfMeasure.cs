using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class UnitOfMeasure
{
    public int UnitOfMeasureId { get; set; }

    public string UnitCode { get; set; } = null!;

    public string UnitName { get; set; } = null!;

    public string? UnitCategory { get; set; }

    public byte DecimalPlaces { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<ItemUnitOfMeasure> ItemUnitOfMeasures { get; set; } = new List<ItemUnitOfMeasure>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
