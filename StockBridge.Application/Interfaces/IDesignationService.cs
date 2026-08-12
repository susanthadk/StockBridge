using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Designations;

namespace StockBridge.Application.Interfaces;

public interface IDesignationService
{
    Task<ResponseInfo<List<DesignationDto>?>> GetAllDesignations(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<DesignationDto>?>> GetAllDesignations(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<DesignationDto?>> GetDesignationById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<DesignationDto>?>> SearchDesignation(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<DesignationDto?>> AddDesignation(DesignationDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateDesignation(DesignationDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteDesignation(int id, CancellationToken cancellationToken = default);
}