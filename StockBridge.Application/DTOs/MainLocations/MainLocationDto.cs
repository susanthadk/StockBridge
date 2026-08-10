using System;

namespace StockBridge.Application.DTOs.MainLocations;

public class MainLocationDto
{
    public int MainLocationId { get; set; }

    public string MainLocCode { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string LocType { get; set; } = string.Empty;

    public string? LocationActiveFlag { get; set; }

    public bool? LinkedToCpu { get; set; }

    public string? DatabaseServer { get; set; }

    public string? DatabaseName { get; set; }

    public string? DatabaseUser { get; set; }

    public string? DatabasePassword { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}