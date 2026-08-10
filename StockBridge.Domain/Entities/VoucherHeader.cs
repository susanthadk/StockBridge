using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class VoucherHeader
{
    public int VoucherHeaderId { get; set; }

    public string VoucherCode { get; set; } = null!;

    public string? VoucherDescriptioncription { get; set; }

    public string? AccountCode { get; set; }

    public string? VoucherFlag { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<VoucherTemporary> VoucherTemporaries { get; set; } = new List<VoucherTemporary>();
}
