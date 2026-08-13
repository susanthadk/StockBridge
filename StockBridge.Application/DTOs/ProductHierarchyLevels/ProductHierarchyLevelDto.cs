using System;

namespace StockBridge.Application.DTOs.ProductHierarchyLevels;

public class ProductHierarchyLevelDto
{
    public int ProductHierarchyLevelId { get; set; }

    public string LevelCode { get; set; } = string.Empty;

    public string LevelName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}