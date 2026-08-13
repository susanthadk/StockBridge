using StockBridge.Application.Common;
using StockBridge.Application.DTOs.OrganizationalUnits;

namespace StockBridge.Application.Interfaces;

public interface IOrganizationalUnitService
{
    Task<ResponseInfo<List<OrganizationalUnitDto>?>> GetAllOrganizationalUnits(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<OrganizationalUnitDto>?>> GetAllOrganizationalUnits(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<OrganizationalUnitDto?>> GetOrganizationalUnitById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<OrganizationalUnitDto>?>> SearchOrganizationalUnit(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<OrganizationalUnitDto?>> AddOrganizationalUnit(OrganizationalUnitDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateOrganizationalUnit(OrganizationalUnitDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteOrganizationalUnit(int id, CancellationToken cancellationToken = default);
}