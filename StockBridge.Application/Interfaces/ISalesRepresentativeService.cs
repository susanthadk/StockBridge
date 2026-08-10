using StockBridge.Application.Common;
using StockBridge.Application.DTOs.SalesRepresentatives;

namespace StockBridge.Application.Interfaces;

public interface ISalesRepresentativeService
{
    Task<ResponseInfo<List<SalesRepresentativeDto>?>> GetAllSalesRepresentatives(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<SalesRepresentativeDto>?>> GetAllSalesRepresentatives(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<SalesRepresentativeDto?>> GetSalesRepresentativeById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<SalesRepresentativeDto>?>> SearchSalesRepresentative(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<SalesRepresentativeDto?>> AddSalesRepresentative(SalesRepresentativeDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateSalesRepresentative(SalesRepresentativeDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteSalesRepresentative(int id, CancellationToken cancellationToken = default);
}