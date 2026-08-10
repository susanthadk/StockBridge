using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class VoucherTemporary
{
    public long VoucherTemporaryId { get; set; }

    public short LineNumber { get; set; }

    public string? VoucherCode { get; set; }

    public string? VoucherTypee { get; set; }

    public string? VoucherDescriptioncription { get; set; }

    public string? VoucherItem { get; set; }

    public string? AccountNumber { get; set; }

    public double? VoucherQuantity { get; set; }

    public double? Price { get; set; }

    public virtual VoucherHeader? VoucherCodeNavigation { get; set; }
}
