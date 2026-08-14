using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class TaxCategory
{
    public int TaxCategoryId { get; set; }

    public string TaxCategoryCode { get; set; } = null!;

    public string TaxCategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsTaxable { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<TaxRate> TaxRates { get; set; } = new List<TaxRate>();
}
