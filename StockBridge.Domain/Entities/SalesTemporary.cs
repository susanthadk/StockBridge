using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class SalesTemporary
{
    public int SalesTemporaryId { get; set; }

    public string? CategoryCode { get; set; }

    public double? SohQuantity { get; set; }

    public double? SohValue { get; set; }

    public double? SaleQuantity { get; set; }

    public double? SaleValue { get; set; }

    public double? RetQuantity { get; set; }

    public double? RetValue { get; set; }
}
