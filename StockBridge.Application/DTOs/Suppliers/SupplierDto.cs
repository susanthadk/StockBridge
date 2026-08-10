using System;

namespace StockBridge.Application.DTOs.Suppliers;

public class SupplierDto
{
    public int SupplierId { get; set; }

    public string SupplierCode { get; set; } = string.Empty;

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
}