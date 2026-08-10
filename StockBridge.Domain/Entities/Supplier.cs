using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierCode { get; set; } = null!;

    public string? SupplierName { get; set; }

    public string? SupplierAdd { get; set; }

    public string? SupplierTp { get; set; }

    public string? SupplierFax { get; set; }

    public string? SupplierMb { get; set; }

    public string? SupplierEmail { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
