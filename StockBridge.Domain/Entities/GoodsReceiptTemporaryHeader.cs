using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class GoodsReceiptTemporaryHeader
{
    public long GoodsReceiptTemporaryHeaderId { get; set; }

    public string GoodsReceiptNumber { get; set; } = null!;

    public int TerminalNumber { get; set; }

    public DateTime? GoodsReceiptDate { get; set; }

    public string? GoodsReceiptSite { get; set; }

    public virtual ICollection<GoodsReceiptTemporaryDetail> GoodsReceiptTemporaryDetails { get; set; } = new List<GoodsReceiptTemporaryDetail>();
}
