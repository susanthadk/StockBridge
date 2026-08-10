using System.ComponentModel.DataAnnotations;

namespace StockBridge.Application.DTOs.GoodsReceiptTemporaries;

public class CreateGoodsReceiptTemporaryDetailDto
{
    [Required(ErrorMessage = "ItmType is required.")]
    [StringLength(7, ErrorMessage = "ItmType cannot exceed 7 characters.")]
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
}

public class CreateGoodsReceiptTemporaryHeaderDto
{
    [Required(ErrorMessage = "GoodsReceiptNumber is required.")]
    [StringLength(10, ErrorMessage = "GoodsReceiptNumber cannot exceed 10 characters.")]
    public string GoodsReceiptNumber { get; set; } = null!;

    [Required(ErrorMessage = "TerminalNumber is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "TerminalNumber must be a positive value.")]
    public int TerminalNumber { get; set; }

    public DateTime? GoodsReceiptDate { get; set; }

    [StringLength(4, ErrorMessage = "GoodsReceiptSite cannot exceed 4 characters.")]
    public string? GoodsReceiptSite { get; set; }

    [Required(ErrorMessage = "At least one detail line is required.")]
    [MinLength(1, ErrorMessage = "At least one detail line is required.")]
    public List<CreateGoodsReceiptTemporaryDetailDto> GoodsReceiptTemporaryDetails { get; set; } = new List<CreateGoodsReceiptTemporaryDetailDto>();
}