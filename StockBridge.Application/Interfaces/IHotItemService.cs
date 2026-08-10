using StockBridge.Application.Common;
using StockBridge.Application.DTOs.HotItems;

namespace StockBridge.Application.Interfaces;

public interface IHotItemService
{
    Task<ResponseInfo<List<HotItemDto>?>> GetAllHotItems(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<HotItemDto>?>> GetAllHotItems(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<HotItemDto?>> GetHotItemById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<HotItemDto>?>> SearchHotItem(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<HotItemDto?>> AddHotItem(HotItemDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateHotItem(HotItemDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteHotItem(int id, CancellationToken cancellationToken = default);
}