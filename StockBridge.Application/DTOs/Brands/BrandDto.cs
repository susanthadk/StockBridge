using System;

namespace StockBridge.Application.DTOs.Brands;

public class BrandDto
{
    public int BrandId { get; set; }

    public string BrandCode { get; set; } = string.Empty;

    public string BrandName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}