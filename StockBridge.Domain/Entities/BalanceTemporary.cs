using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class BalanceTemporary
{
    public int BalanceTemporaryId { get; set; }

    public DateTime? Date { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Value { get; set; }

    public string? Text { get; set; }

    public string? Type { get; set; }

    public string? Flag { get; set; }
}
