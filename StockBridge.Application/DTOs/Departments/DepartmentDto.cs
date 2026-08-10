using System;

namespace StockBridge.Application.DTOs.Departments;

public class DepartmentDto
{
    public int DepartmentId { get; set; }

    public string DepartmentCode { get; set; } = string.Empty;

    public string? DepartmentName { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}