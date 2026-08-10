using System.ComponentModel.DataAnnotations;

namespace StockBridge.Domain.Entities;

public class BaseEntity
{
    [Required]
    public int CreatedBy { get; set; }
    [Required]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public int? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
}