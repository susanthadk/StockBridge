using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class ProductHierarchyLevel
{
    public int ProductHierarchyLevelId { get; set; }

    public string LevelCode { get; set; } = null!;

    public string LevelName { get; set; } = null!;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<ProductHierarchy> ProductHierarchies { get; set; } = new List<ProductHierarchy>();
}
