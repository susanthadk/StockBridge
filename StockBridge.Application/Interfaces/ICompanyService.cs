using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Companies;

namespace StockBridge.Application.Interfaces;

public interface ICompanyService
{
    Task<ResponseInfo<List<CompanyDto>?>> GetAllCompanies(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<CompanyDto>?>> GetAllCompanies(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<CompanyDto?>> GetCompanyById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<CompanyDto>?>> SearchCompany(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<CompanyDto?>> AddCompany(CompanyDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateCompany(CompanyDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteCompany(int id, CancellationToken cancellationToken = default);
}