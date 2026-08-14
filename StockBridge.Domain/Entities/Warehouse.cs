using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public string WarehouseCode { get; set; } = null!;

    public string WarehouseName { get; set; } = null!;

    public int? WarehouseTypeId { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? PostalCode { get; set; }

    public string? CountryCode { get; set; }

    public string? TelephoneNumber { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmailAddress { get; set; }

    public bool IsCentralWarehouse { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<ItemWarehouse> ItemWarehouses { get; set; } = new List<ItemWarehouse>();

    public virtual WarehouseType? WarehouseType { get; set; }
}
