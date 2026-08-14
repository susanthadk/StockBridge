using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class ItemPrice
{
    public long ItemPriceId { get; set; }

    public int ItemId { get; set; }

    public int ItemUnitOfMeasureId { get; set; }

    public string PriceType { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public DateOnly EffectiveFrom { get; set; }

    public DateOnly? EffectiveTo { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ItemUnitOfMeasure ItemUnitOfMeasure { get; set; } = null!;
}
