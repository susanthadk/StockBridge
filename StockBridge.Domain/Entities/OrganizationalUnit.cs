using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class OrganizationalUnit
{
    public int OrganizationalUnitId { get; set; }

    public string OrganizationalUnitCode { get; set; } = null!;

    public string OrganizationalUnitName { get; set; } = null!;

    public int LevelId { get; set; }

    public int? ParentUnitId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<OrganizationalUnit> InverseParentUnit { get; set; } = new List<OrganizationalUnit>();

    public virtual OrganizationalLevel Level { get; set; } = null!;

    public virtual OrganizationalUnit? ParentUnit { get; set; }
}
