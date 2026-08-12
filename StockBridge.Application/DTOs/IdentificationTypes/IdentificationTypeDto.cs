using System;

namespace StockBridge.Application.DTOs.IdentificationTypes;

public class IdentificationTypeDto
{
    public int IdentificationTypeId { get; set; }

    public string IdentificationTypeCode { get; set; } = string.Empty;

    public string IdentificationTypeName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}