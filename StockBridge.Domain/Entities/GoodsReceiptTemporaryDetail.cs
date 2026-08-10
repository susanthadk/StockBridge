using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class GoodsReceiptTemporaryDetail
{
    public long GoodsReceiptTemporaryDetailId { get; set; }

    public string GoodsReceiptNumber { get; set; } = null!;

    public int TerminalNumber { get; set; }

    public DateTime? GoodsReceiptDate { get; set; }

    public string ItmType { get; set; } = null!;

    public decimal? GoodsReceiptQuantity { get; set; }

    public decimal? GoodsReceiptSellingPrice { get; set; }

    public short? GoodsReceiptS00 { get; set; }

    public short? GoodsReceiptS01 { get; set; }

    public short? GoodsReceiptS02 { get; set; }

    public short? GoodsReceiptS03 { get; set; }

    public short? GoodsReceiptS04 { get; set; }

    public short? GoodsReceiptS05 { get; set; }

    public short? GoodsReceiptS06 { get; set; }

    public short? GoodsReceiptS07 { get; set; }

    public short? GoodsReceiptS08 { get; set; }

    public short? GoodsReceiptS09 { get; set; }

    public short? GoodsReceiptS10 { get; set; }

    public short? GoodsReceiptS11 { get; set; }

    public short? GoodsReceiptS12 { get; set; }

    public short? GoodsReceiptS13 { get; set; }

    public short? GoodsReceiptS14 { get; set; }

    public int? GoodsReceiptQ00 { get; set; }

    public int? GoodsReceiptQ01 { get; set; }

    public int? GoodsReceiptQ02 { get; set; }

    public int? GoodsReceiptQ03 { get; set; }

    public int? GoodsReceiptQ04 { get; set; }

    public int? GoodsReceiptQ05 { get; set; }

    public int? GoodsReceiptQ06 { get; set; }

    public int? GoodsReceiptQ07 { get; set; }

    public int? GoodsReceiptQ08 { get; set; }

    public int? GoodsReceiptQ09 { get; set; }

    public int? GoodsReceiptQ10 { get; set; }

    public int? GoodsReceiptQ11 { get; set; }

    public int? GoodsReceiptQ12 { get; set; }

    public int? GoodsReceiptQ13 { get; set; }

    public int? GoodsReceiptQ14 { get; set; }

    public virtual GoodsReceiptTemporaryHeader GoodsReceiptTemporaryHeader { get; set; } = null!;
}
