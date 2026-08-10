using StockBridge.Application.Common;
using StockBridge.Application.DTOs.GoodsReceiptTemporaries;

namespace StockBridge.Application.Interfaces;

public interface IGoodsReceiptTemporaryService
{
    Task<ResponseInfo<List<GoodsReceiptTemporaryHeaderDto>?>> GetAllGoodsReceiptTemporaries(CancellationToken cancellationToken = default);
    Task<ResponseInfo<GoodsReceiptTemporaryHeaderDto?>> GetGoodsReceiptTemporaryById(long headerId, CancellationToken cancellationToken = default);
    Task<ResponseInfo<GoodsReceiptTemporaryHeaderDto?>> AddGoodsReceiptTemporary(CreateGoodsReceiptTemporaryHeaderDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<GoodsReceiptTemporaryHeaderDto?>> UpdateGoodsReceiptTemporary(UpdateGoodsReceiptTemporaryHeaderDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteGoodsReceiptTemporary(long headerId, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> IsExist(long headerId, CancellationToken cancellationToken = default);
}