using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class HeaderWriteTemporary
{
    public long HeaderWriteTemporaryId { get; set; }

    public int LineNumber { get; set; }

    public string? SalesmanCode { get; set; }

    public string? ItemNumber { get; set; }

    public string? ItemType { get; set; }

    public string? ItemDescriptioncription { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Value { get; set; }

    public decimal? DiscountRate { get; set; }

    public string? IsDeleted { get; set; }

    public string? IsReturn { get; set; }

    public string? DiscountGroup { get; set; }

    public decimal? GpDisRate { get; set; }

    public string? GpDisTyp { get; set; }

    public string? GpPrint { get; set; }

    public string? GpPrintQuantity { get; set; }

    public string? CatCode { get; set; }

    public int TerminalNumber { get; set; }
}
