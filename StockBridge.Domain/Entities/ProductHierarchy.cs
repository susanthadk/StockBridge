using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class ProductHierarchy
{
    public int ProductHierarchyId { get; set; }

    public int ProductHierarchyLevelId { get; set; }

    public int? ParentProductHierarchyId { get; set; }

    public string ProductHierarchyCode { get; set; } = null!;

    public string ProductHierarchyName { get; set; } = null!;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<ProductHierarchy> InverseParentProductHierarchy { get; set; } = new List<ProductHierarchy>();

    public virtual ProductHierarchy? ParentProductHierarchy { get; set; }

    public virtual ProductHierarchyLevel ProductHierarchyLevel { get; set; } = null!;
}
