using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Family
{
    public int FamilyId { get; set; }

    public string FamilyCode { get; set; } = null!;

    public string CategoryCode { get; set; } = null!;

    public string DepartmentCode { get; set; } = null!;

    public string? FamilyName { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
