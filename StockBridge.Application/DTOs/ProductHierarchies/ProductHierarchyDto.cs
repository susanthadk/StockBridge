using System;

namespace StockBridge.Application.DTOs.ProductHierarchies;

public class ProductHierarchyDto
{
    public int ProductHierarchyId { get; set; }

    public int ProductHierarchyLevelId { get; set; }

    public int? ParentProductHierarchyId { get; set; }

    public string ProductHierarchyCode { get; set; } = string.Empty;

    public string ProductHierarchyName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}