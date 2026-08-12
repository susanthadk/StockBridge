using System;

namespace StockBridge.Application.DTOs.SupplierTypes;

public class SupplierTypeDto
{
    public int SupplierTypeId { get; set; }

    public string SupplierTypeCode { get; set; } = string.Empty;

    public string SupplierTypeName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
