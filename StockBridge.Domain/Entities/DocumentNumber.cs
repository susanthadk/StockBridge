using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class DocumentNumber
{
    public int DocumentNumberId { get; set; }

    public string MainLocationCode { get; set; } = null!;

    public string StationId { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public string NumberId { get; set; } = null!;

    public int DocumentNumber1 { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual MainLocation MainLocationCodeNavigation { get; set; } = null!;
}
