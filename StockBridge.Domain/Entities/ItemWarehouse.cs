using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class ItemWarehouse
{
    public int ItemWarehouseId { get; set; }

    public int ItemId { get; set; }

    public int WarehouseId { get; set; }

    public decimal? MinimumStockQuantity { get; set; }

    public decimal? MaximumStockQuantity { get; set; }

    public decimal? ReorderLevelQuantity { get; set; }

    public decimal? ReorderQuantity { get; set; }

    public int? DefaultStorageLocationId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual Warehouse Warehouse { get; set; } = null!;
}
