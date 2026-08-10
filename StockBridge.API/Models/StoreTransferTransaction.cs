using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class StoreTransferTransaction
{
    public long StoreTransferTransactionId { get; set; }

    public string GoodsOutNoteNumber { get; set; } = null!;

    public DateTime? GoodsOutNoteDate { get; set; }

    public decimal GoodsOutNoteQuantity { get; set; }

    public decimal GoodsOutNoteValue { get; set; }

    public string GoodsInNoteNumber { get; set; } = null!;

    public DateTime? GoodsInNoteDate { get; set; }

    public decimal GoodsInNoteQuantity { get; set; }

    public decimal GoodsInNoteValue { get; set; }

    public string ItemCode { get; set; } = null!;

    public string StockCode { get; set; } = null!;

    public decimal SellingPrice { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual Item ItemCodeNavigation { get; set; } = null!;

    public virtual Stock StockCodeNavigation { get; set; } = null!;
}
