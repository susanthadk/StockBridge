using StockBridge.Application.Common;
using StockBridge.Application.DTOs.MultiItems;

namespace StockBridge.Application.Interfaces;

public interface IMultiItemService
{
    Task<ResponseInfo<List<MultiItemDto>?>> GetAllMultiItems(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<MultiItemDto>?>> GetAllMultiItems(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<MultiItemDto?>> GetMultiItemById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<MultiItemDto>?>> SearchMultiItem(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<MultiItemDto?>> AddMultiItem(MultiItemDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateMultiItem(MultiItemDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteMultiItem(int id, CancellationToken cancellationToken = default);
}