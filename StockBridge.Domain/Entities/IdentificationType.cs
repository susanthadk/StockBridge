using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class IdentificationType
{
    public int IdentificationTypeId { get; set; }

    public string IdentificationTypeCode { get; set; } = null!;

    public string IdentificationTypeName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
