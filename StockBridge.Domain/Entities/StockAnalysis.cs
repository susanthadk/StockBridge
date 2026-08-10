using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class StockAnalysis
{
    public int StockAnalysisId { get; set; }

    public string ItemType { get; set; } = null!;

    public decimal? StockAsDate { get; set; }

    public decimal? SaleEsAsDate { get; set; }

    public decimal? GoodsReceiptAsDate { get; set; }

    public decimal? GoodsInNoteAsDate { get; set; }

    public decimal? RetAsDate { get; set; }

    public decimal? OpenStk { get; set; }

    public decimal? Sales { get; set; }

    public decimal? Grn { get; set; }

    public decimal? Gin { get; set; }

    public decimal? SaleReturn { get; set; }

    public decimal? CloseStk { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
