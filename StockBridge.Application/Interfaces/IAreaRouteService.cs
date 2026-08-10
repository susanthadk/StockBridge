using StockBridge.Application.Common;
using StockBridge.Application.DTOs.AreaRoutes;

namespace StockBridge.Application.Interfaces;

public interface IAreaRouteService
{
    Task<ResponseInfo<List<AreaRouteDto>?>> GetAllAreaRoutes(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<AreaRouteDto>?>> GetAllAreaRoutes(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<AreaRouteDto?>> GetAreaRouteById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<AreaRouteDto>?>> SearchAreaRoute(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<AreaRouteDto?>> AddAreaRoute(AreaRouteDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateAreaRoute(AreaRouteDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteAreaRoute(int id, CancellationToken cancellationToken = default);
}