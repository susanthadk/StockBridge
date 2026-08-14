using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class ItemBarcode
{
    public long ItemBarcodeId { get; set; }

    public int ItemId { get; set; }

    public int? ItemUnitOfMeasureId { get; set; }

    public string Barcode { get; set; } = null!;

    public string? BarcodeType { get; set; }

    public bool IsPrimary { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ItemUnitOfMeasure? ItemUnitOfMeasure { get; set; }
}
