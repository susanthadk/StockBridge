using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Companies;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly IRepository<Company> _companyRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<CompanyService> _logger;

    public CompanyService(
        IRepository<Company> companyRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<CompanyService> logger)
    {
        _companyRepository = companyRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<CompanyDto>?>> GetAllCompanies(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Companies.");

        var result = await _companyRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Companies found.", nameof(CompanyService), nameof(GetAllCompanies));
            return ResponseInfo<List<CompanyDto>?>.Success(new List<CompanyDto>(), HttpStatusCode.NoContent, "No Companies found.");
        }

        var dtos = _mapper.Map<List<CompanyDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Companies.", nameof(CompanyService), nameof(GetAllCompanies), dtos.Count);

        return ResponseInfo<List<CompanyDto>?>.Success(dtos, HttpStatusCode.OK, "Companies retrieved successfully.");
    }

    public async Task<ResponseInfo<List<CompanyDto>?>> GetAllCompanies(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Companies. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _companyRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Companies found.", nameof(CompanyService), nameof(GetAllCompanies));
            return ResponseInfo<List<CompanyDto>?>.Success(new List<CompanyDto>(), HttpStatusCode.NoContent, "No Companies found.");
        }

        var dtos = _mapper.Map<List<CompanyDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Companies.", nameof(CompanyService), nameof(GetAllCompanies), dtos.Count);

        return ResponseInfo<List<CompanyDto>?>.Success(dtos, HttpStatusCode.OK, "Companies retrieved successfully.");
    }

    public async Task<ResponseInfo<CompanyDto?>> GetCompanyById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Company Id: {CompanyId}", id);

        var result = await _companyRepository.GetByIdAsync(id, nameof(Company.CompanyId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Company not found Id: {CompanyId}.", nameof(CompanyService), nameof(GetCompanyById), id);
            return ResponseInfo<CompanyDto?>.Success(null, HttpStatusCode.NoContent, "Company not found.");
        }

        var dto = _mapper.Map<CompanyDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Company Id: {CompanyId}.", nameof(CompanyService), nameof(GetCompanyById), id);

        return ResponseInfo<CompanyDto?>.Success(dto, HttpStatusCode.OK, "Company retrieved successfully.");
    }

    public async Task<ResponseInfo<List<CompanyDto>?>> SearchCompany(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching Companies by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _companyRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Companies found.", nameof(CompanyService), nameof(SearchCompany));
            return ResponseInfo<List<CompanyDto>?>.Success(new List<CompanyDto>(), HttpStatusCode.NoContent, "No Companies found.");
        }

        var dtos = _mapper.Map<List<CompanyDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Companies.", nameof(CompanyService), nameof(SearchCompany), dtos.Count);

        return ResponseInfo<List<CompanyDto>?>.Success(dtos, HttpStatusCode.OK, "Companies retrieved successfully.");
    }

    public async Task<ResponseInfo<CompanyDto?>> AddCompany(CompanyDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Company.");

        var existing = await _companyRepository.GetByFieldAsync("LocationNumber", dto.LocationNumber);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Company already exists with the same Location Number.", nameof(CompanyService), nameof(AddCompany));
            return ResponseInfo<CompanyDto?>.Failure("Company already exists with the same Location Number.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Company>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _companyRepository.AddAsync(entity);

        var resultDto = _mapper.Map<CompanyDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Company added successfully CompanyId: {CompanyId}.", nameof(CompanyService), nameof(AddCompany), result.CompanyId);

        return ResponseInfo<CompanyDto?>.Success(resultDto, HttpStatusCode.Created, "Company added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateCompany(CompanyDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Company Id: {CompanyId}", dto.CompanyId);

        var isExists = await _companyRepository.IsExistByIdAsync(dto.CompanyId, nameof(Company.CompanyId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Company not found Id: {CompanyId}.", nameof(CompanyService), nameof(UpdateCompany), dto.CompanyId);
            return ResponseInfo<bool>.Failure("Company not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<Company>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _companyRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Company updated Id: {CompanyId}.", nameof(CompanyService), nameof(UpdateCompany), dto.CompanyId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Company updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteCompany(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Company Id: {CompanyId}", id);

        var isExists = await _companyRepository.IsExistByIdAsync(id, nameof(Company.CompanyId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Company not found Id: {CompanyId}.", nameof(CompanyService), nameof(DeleteCompany), id);
            return ResponseInfo<bool>.Failure("Company not found.", HttpStatusCode.NotFound);
        }

        await _companyRepository.DeleteAsync(id, nameof(Company.CompanyId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Company deleted Id: {CompanyId}.", nameof(CompanyService), nameof(DeleteCompany), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Company deleted successfully.");
    }
}