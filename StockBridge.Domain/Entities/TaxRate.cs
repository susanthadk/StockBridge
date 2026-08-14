using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class TaxRate
{
    public int TaxRateId { get; set; }

    public int TaxCategoryId { get; set; }

    public decimal TaxRatePercentage { get; set; }

    public DateOnly EffectiveFrom { get; set; }

    public DateOnly? EffectiveTo { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual TaxCategory TaxCategory { get; set; } = null!;
}
