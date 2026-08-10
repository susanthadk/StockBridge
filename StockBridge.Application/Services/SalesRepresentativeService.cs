using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.SalesRepresentatives;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class SalesRepresentativeService : ISalesRepresentativeService
{
    private readonly IRepository<SalesRepresentative> _salesRepresentativeRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<SalesRepresentativeService> _logger;

    public SalesRepresentativeService(
        IRepository<SalesRepresentative> salesRepresentativeRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<SalesRepresentativeService> logger)
    {
        _salesRepresentativeRepository = salesRepresentativeRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<SalesRepresentativeDto>?>> GetAllSalesRepresentatives(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all SalesRepresentatives.");

        var result = await _salesRepresentativeRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No SalesRepresentatives found.", nameof(SalesRepresentativeService), nameof(GetAllSalesRepresentatives));
            return ResponseInfo<List<SalesRepresentativeDto>?>.Success(new List<SalesRepresentativeDto>(), HttpStatusCode.NoContent, "No SalesRepresentatives found.");
        }

        var dtos = _mapper.Map<List<SalesRepresentativeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} SalesRepresentatives.", nameof(SalesRepresentativeService), nameof(GetAllSalesRepresentatives), dtos.Count);

        return ResponseInfo<List<SalesRepresentativeDto>?>.Success(dtos, HttpStatusCode.OK, "SalesRepresentatives retrieved successfully.");
    }

    public async Task<ResponseInfo<List<SalesRepresentativeDto>?>> GetAllSalesRepresentatives(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all SalesRepresentatives. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _salesRepresentativeRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No SalesRepresentatives found.", nameof(SalesRepresentativeService), nameof(GetAllSalesRepresentatives));
            return ResponseInfo<List<SalesRepresentativeDto>?>.Success(new List<SalesRepresentativeDto>(), HttpStatusCode.NoContent, "No SalesRepresentatives found.");
        }

        var dtos = _mapper.Map<List<SalesRepresentativeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} SalesRepresentatives.", nameof(SalesRepresentativeService), nameof(GetAllSalesRepresentatives), dtos.Count);

        return ResponseInfo<List<SalesRepresentativeDto>?>.Success(dtos, HttpStatusCode.OK, "SalesRepresentatives retrieved successfully.");
    }

    public async Task<ResponseInfo<SalesRepresentativeDto?>> GetSalesRepresentativeById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching SalesRepresentative Id: {SalesRepresentativeId}", id);

        var result = await _salesRepresentativeRepository.GetByIdAsync(id, nameof(SalesRepresentative.SalesRepresentativeId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: SalesRepresentative not found Id: {SalesRepresentativeId}.", nameof(SalesRepresentativeService), nameof(GetSalesRepresentativeById), id);
            return ResponseInfo<SalesRepresentativeDto?>.Success(null, HttpStatusCode.NoContent, "SalesRepresentative not found.");
        }

        var dto = _mapper.Map<SalesRepresentativeDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved SalesRepresentative Id: {SalesRepresentativeId}.", nameof(SalesRepresentativeService), nameof(GetSalesRepresentativeById), id);

        return ResponseInfo<SalesRepresentativeDto?>.Success(dto, HttpStatusCode.OK, "SalesRepresentative retrieved successfully.");
    }

    public async Task<ResponseInfo<List<SalesRepresentativeDto>?>> SearchSalesRepresentative(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching SalesRepresentatives by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _salesRepresentativeRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No SalesRepresentatives found.", nameof(SalesRepresentativeService), nameof(SearchSalesRepresentative));
            return ResponseInfo<List<SalesRepresentativeDto>?>.Success(new List<SalesRepresentativeDto>(), HttpStatusCode.NoContent, "No SalesRepresentatives found.");
        }

        var dtos = _mapper.Map<List<SalesRepresentativeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} SalesRepresentatives.", nameof(SalesRepresentativeService), nameof(SearchSalesRepresentative), dtos.Count);

        return ResponseInfo<List<SalesRepresentativeDto>?>.Success(dtos, HttpStatusCode.OK, "SalesRepresentatives retrieved successfully.");
    }

    public async Task<ResponseInfo<SalesRepresentativeDto?>> AddSalesRepresentative(SalesRepresentativeDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding SalesRepresentative.");

        var existing = await _salesRepresentativeRepository.GetByFieldAsync("SalesRepresentativeresentativeCode", dto.SalesRepresentativeresentativeCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: SalesRepresentative already exists with the same Code.", nameof(SalesRepresentativeService), nameof(AddSalesRepresentative));
            return ResponseInfo<SalesRepresentativeDto?>.Failure("SalesRepresentative already exists with the same Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<SalesRepresentative>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _salesRepresentativeRepository.AddAsync(entity);

        var resultDto = _mapper.Map<SalesRepresentativeDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: SalesRepresentative added successfully SalesRepresentativeId: {SalesRepresentativeId}.", nameof(SalesRepresentativeService), nameof(AddSalesRepresentative), result.SalesRepresentativeId);

        return ResponseInfo<SalesRepresentativeDto?>.Success(resultDto, HttpStatusCode.Created, "SalesRepresentative added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateSalesRepresentative(SalesRepresentativeDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating SalesRepresentative Id: {SalesRepresentativeId}", dto.SalesRepresentativeId);

        var isExists = await _salesRepresentativeRepository.IsExistByIdAsync(dto.SalesRepresentativeId, nameof(SalesRepresentative.SalesRepresentativeId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: SalesRepresentative not found Id: {SalesRepresentativeId}.", nameof(SalesRepresentativeService), nameof(UpdateSalesRepresentative), dto.SalesRepresentativeId);
            return ResponseInfo<bool>.Failure("SalesRepresentative not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<SalesRepresentative>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _salesRepresentativeRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: SalesRepresentative updated Id: {SalesRepresentativeId}.", nameof(SalesRepresentativeService), nameof(UpdateSalesRepresentative), dto.SalesRepresentativeId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "SalesRepresentative updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteSalesRepresentative(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting SalesRepresentative Id: {SalesRepresentativeId}", id);

        var isExists = await _salesRepresentativeRepository.IsExistByIdAsync(id, nameof(SalesRepresentative.SalesRepresentativeId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: SalesRepresentative not found Id: {SalesRepresentativeId}.", nameof(SalesRepresentativeService), nameof(DeleteSalesRepresentative), id);
            return ResponseInfo<bool>.Failure("SalesRepresentative not found.", HttpStatusCode.NotFound);
        }

        await _salesRepresentativeRepository.DeleteAsync(id, nameof(SalesRepresentative.SalesRepresentativeId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: SalesRepresentative deleted Id: {SalesRepresentativeId}.", nameof(SalesRepresentativeService), nameof(DeleteSalesRepresentative), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "SalesRepresentative deleted successfully.");
    }
}