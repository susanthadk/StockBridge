using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Designation
{
    public int DesignationId { get; set; }

    public string DesignationCode { get; set; } = null!;

    public string DesignationName { get; set; } = null!;

    public string? Description { get; set; }

    public int? DepartmentId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
