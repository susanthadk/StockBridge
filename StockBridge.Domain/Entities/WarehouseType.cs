using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class WarehouseType
{
    public int WarehouseTypeId { get; set; }

    public string WarehouseTypeCode { get; set; } = null!;

    public string WarehouseTypeName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
