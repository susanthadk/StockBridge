using System;

namespace StockBridge.Application.DTOs.Categories;

public class CategoryDto
{
    public int CategoryId { get; set; }

    public string DepartmentCode { get; set; } = string.Empty;

    public string CategoryCode { get; set; } = string.Empty;

    public string? CategoryName { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}