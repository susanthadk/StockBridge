using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentCode { get; set; } = null!;

    public string? DepartmentName { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
