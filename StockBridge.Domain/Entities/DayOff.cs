using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class DayOff
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
