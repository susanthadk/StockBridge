using System;

namespace StockBridge.Application.DTOs.DayOffs;

public class DayOffDto
{
    public int DayOffId { get; set; }

    public DateTime? OffDate { get; set; }

    public string? OffNumber { get; set; }

    public DateTime? OffTimee { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}