using StockBridge.Application.Common;
using StockBridge.Application.DTOs.DayOffs;

namespace StockBridge.Application.Interfaces;

public interface IDayOffService
{
    Task<ResponseInfo<List<DayOffDto>?>> GetAllDayOffs(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<DayOffDto>?>> GetAllDayOffs(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<DayOffDto?>> GetDayOffById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<DayOffDto>?>> SearchDayOff(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<DayOffDto?>> AddDayOff(DayOffDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateDayOff(DayOffDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteDayOff(int id, CancellationToken cancellationToken = default);
}