using System;

namespace StockBridge.Application.DTOs.DeliveryMethods;

public class DeliveryMethodDto
{
    public int DeliveryMethodId { get; set; }

    public string DeliveryMethodCode { get; set; } = string.Empty;

    public string DeliveryMethodName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
