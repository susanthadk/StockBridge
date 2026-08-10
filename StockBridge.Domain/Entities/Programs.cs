using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class Programs
{
    public int ProgramId { get; set; }

    public string ProgramCode { get; set; } = null!;

    public string? ProgramDescriptioncription { get; set; }

    public string? Menu { get; set; }

    public string? ProgramType { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
