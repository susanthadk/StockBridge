using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string DepartmentCode { get; set; } = null!;

    public string CategoryCode { get; set; } = null!;

    public string? CategoryName { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Department DepartmentCodeNavigation { get; set; } = null!;

    public virtual ICollection<Family> Families { get; set; } = new List<Family>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
