using StockBridge.Application.Common;
using StockBridge.Application.DTOs.MainLocations;

namespace StockBridge.Application.Interfaces;

public interface IMainLocationService
{
    Task<ResponseInfo<List<MainLocationDto>?>> GetAllMainLocations(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<MainLocationDto>?>> GetAllMainLocations(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<MainLocationDto?>> GetMainLocationById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<MainLocationDto>?>> SearchMainLocation(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<MainLocationDto?>> AddMainLocation(MainLocationDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateMainLocation(MainLocationDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteMainLocation(int id, CancellationToken cancellationToken = default);
}