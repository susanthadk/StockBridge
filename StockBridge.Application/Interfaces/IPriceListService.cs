using StockBridge.Application.Common;
using StockBridge.Application.DTOs.PriceLists;

namespace StockBridge.Application.Interfaces;

public interface IPriceListService
{
    Task<ResponseInfo<List<PriceListDto>?>> GetAllPriceLists(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<PriceListDto>?>> GetAllPriceLists(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<PriceListDto?>> GetPriceListById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<PriceListDto>?>> SearchPriceList(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<PriceListDto?>> AddPriceList(PriceListDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdatePriceList(PriceListDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeletePriceList(int id, CancellationToken cancellationToken = default);
}