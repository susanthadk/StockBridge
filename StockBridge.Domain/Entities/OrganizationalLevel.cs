using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class OrganizationalLevel
{
    public int LevelId { get; set; }

    public string OrganizationLevel { get; set; } = null!;

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<OrganizationalUnit> OrganizationalUnits { get; set; } = new List<OrganizationalUnit>();
}
