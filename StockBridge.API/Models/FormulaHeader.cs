using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class FormulaHeader
{
    public int FormulaHeaderId { get; set; }

    public string FormulaNumber { get; set; } = null!;

    public DateTime? FormulaDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<FormulaLine> FormulaLines { get; set; } = new List<FormulaLine>();
}
