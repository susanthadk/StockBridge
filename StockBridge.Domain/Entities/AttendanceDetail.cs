using System;
using System.Collections.Generic;

namespace StockBridge.API.Models;

public partial class AttendanceDetail
{
    public long AttendanceDetailId { get; set; }

    public string EmployeeProvidentFundNumber { get; set; } = null!;

    public DateTime InDate { get; set; }

    public DateTime? InTimee { get; set; }

    public DateTime? OutDate { get; set; }

    public DateTime? OutTimee { get; set; }

    public string? LoginCode { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsActive { get; set; }
}
