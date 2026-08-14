using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class ItemUnitOfMeasure
{
    public int ItemUnitOfMeasureId { get; set; }

    public int ItemId { get; set; }

    public int UnitOfMeasureId { get; set; }

    public decimal ConversionFactor { get; set; }

    public bool IsBaseUnit { get; set; }

    public bool IsPurchaseUnit { get; set; }

    public bool IsSalesUnit { get; set; }

    public bool IsStockUnit { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<ItemBarcode> ItemBarcodes { get; set; } = new List<ItemBarcode>();

    public virtual ICollection<ItemPrice> ItemPrices { get; set; } = new List<ItemPrice>();

    public virtual ICollection<ItemSupplier> ItemSuppliers { get; set; } = new List<ItemSupplier>();

    public virtual UnitOfMeasure UnitOfMeasure { get; set; } = null!;
}
