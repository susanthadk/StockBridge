using System;

namespace StockBridge.Application.DTOs.AreaRoutes;

public class AreaRouteDto
{
    public int AreaRouteId { get; set; }

    public string AreaCode { get; set; } = string.Empty;

    public string? AreaName { get; set; }

    public string? ShortName { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}