using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class SupplierType
{
    public int SupplierTypeId { get; set; }

    public string SupplierTypeCode { get; set; } = null!;

    public string SupplierTypeName { get; set; } = null!;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
