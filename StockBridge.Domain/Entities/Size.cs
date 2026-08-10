using System;
using System.Collections.Generic;

namespace StockBridge.Domain.Entities;

public partial class Size
{
    public int SizeId { get; set; }

    public short SizeNumber { get; set; }

    public short? SizeValue01 { get; set; }

    public short? SizeValue02 { get; set; }

    public short? SizeValue03 { get; set; }

    public short? SizeValue04 { get; set; }

    public short? SizeValue05 { get; set; }

    public short? SizeValue06 { get; set; }

    public short? SizeValue07 { get; set; }

    public short? SizeValue08 { get; set; }

    public short? SizeValue09 { get; set; }

    public short? SizeValue10 { get; set; }

    public short? SizeValue11 { get; set; }

    public short? SizeValue12 { get; set; }

    public short? SizeValue13 { get; set; }

    public short? SizeValue14 { get; set; }

    public string? EntryUserCode { get; set; }

    public DateTime? EntryDate { get; set; }

    public DateTime? EntryTime { get; set; }

    public string? AmendedByUserCode { get; set; }

    public DateTime? AmendedDate { get; set; }

    public DateTime? AmendedTime { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<InventoryLineTransaction> InventoryLineTransactions { get; set; } = new List<InventoryLineTransaction>();

    public virtual ICollection<InventoryWarehouseTransactionReturn> InventoryWarehouseTransactionReturns { get; set; } = new List<InventoryWarehouseTransactionReturn>();

    public virtual ICollection<InventoryWarehouseTransaction> InventoryWarehouseTransactions { get; set; } = new List<InventoryWarehouseTransaction>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<VoucherInventoryLine> VoucherInventoryLines { get; set; } = new List<VoucherInventoryLine>();
}
