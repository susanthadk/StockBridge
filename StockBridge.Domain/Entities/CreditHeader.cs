using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class CreditHeader
{
    public int CreditHeaderId { get; set; }

    public string CreditCode { get; set; } = null!;

    public string? CreditDescriptioncription { get; set; }

    public double? CreditRate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<CreditSale> CreditSales { get; set; } = new List<CreditSale>();
}
