using System;

namespace StockBridge.Application.DTOs.Designations;

public class DesignationDto
{
    public int DesignationId { get; set; }

    public string DesignationCode { get; set; } = string.Empty;

    public string DesignationName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int? DepartmentId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}