using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class BarcodeLabelDatum
{
    public int BarcodeLabelDataId { get; set; }

    public string? ItemCode { get; set; }

    public string? StockCode { get; set; }

    public string? StockDescriptioncription { get; set; }

    public decimal? SellingPrice { get; set; }

    public string? SellingPriceDescriptioncription { get; set; }

    public string? InitialCreate { get; set; }
}
