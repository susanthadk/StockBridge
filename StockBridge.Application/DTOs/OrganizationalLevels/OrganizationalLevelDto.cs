using System;

namespace StockBridge.Application.DTOs.OrganizationalLevels;

public class OrganizationalLevelDto
{
    public int LevelId { get; set; }

    public string OrganizationLevel { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}