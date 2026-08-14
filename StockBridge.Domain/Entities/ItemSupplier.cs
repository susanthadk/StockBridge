using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class ItemSupplier
{
    public int ItemSupplierId { get; set; }

    public int ItemId { get; set; }

    public int SupplierId { get; set; }

    public string? SupplierItemCode { get; set; }

    public int? PurchaseUnitOfMeasureId { get; set; }

    public decimal? PurchasePrice { get; set; }

    public decimal? MinimumOrderQuantity { get; set; }

    public decimal? OrderMultipleQuantity { get; set; }

    public int? LeadTimeDays { get; set; }

    public bool IsPrimarySupplier { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ItemUnitOfMeasure? PurchaseUnitOfMeasure { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}
