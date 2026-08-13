using StockBridge.Application.Common;
using StockBridge.Application.DTOs.OrganizationalLevels;

namespace StockBridge.Application.Interfaces;

public interface IOrganizationalLevelService
{
    Task<ResponseInfo<List<OrganizationalLevelDto>?>> GetAllOrganizationalLevels(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<OrganizationalLevelDto>?>> GetAllOrganizationalLevels(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<OrganizationalLevelDto?>> GetOrganizationalLevelById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<OrganizationalLevelDto>?>> SearchOrganizationalLevel(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<OrganizationalLevelDto?>> AddOrganizationalLevel(OrganizationalLevelDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateOrganizationalLevel(OrganizationalLevelDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteOrganizationalLevel(int id, CancellationToken cancellationToken = default);
}