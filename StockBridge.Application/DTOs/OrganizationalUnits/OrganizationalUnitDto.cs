using System;

namespace StockBridge.Application.DTOs.OrganizationalUnits;

public class OrganizationalUnitDto
{
    public int OrganizationalUnitId { get; set; }

    public string OrganizationalUnitCode { get; set; } = string.Empty;

    public string OrganizationalUnitName { get; set; } = string.Empty;

    public int LevelId { get; set; }

    public int? ParentUnitId { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}