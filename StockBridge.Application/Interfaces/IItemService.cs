using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Items;

namespace StockBridge.Application.Interfaces;

public interface IItemService
{
    Task<ResponseInfo<List<ItemDto>?>> GetAllItems(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<ItemDto>?>> GetAllItems(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<ItemDto?>> GetItemById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<ItemDto>?>> SearchItem(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<ItemDto?>> AddItem(ItemDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateItem(ItemDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteItem(int id, CancellationToken cancellationToken = default);
}