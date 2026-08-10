using System;

namespace StockBridge.Application.DTOs.HotItems;

public class HotItemDto
{
    public int HotItemId { get; set; }

    public string? ItemCode { get; set; }

    public string? Descriptioncription { get; set; }

    public decimal? Quantity { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}