using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.OrganizationalUnits;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class OrganizationalUnitService : IOrganizationalUnitService
{
    private readonly IRepository<OrganizationalUnit> _organizationalUnitRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<OrganizationalUnitService> _logger;

    public OrganizationalUnitService(
        IRepository<OrganizationalUnit> organizationalUnitRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<OrganizationalUnitService> logger)
    {
        _organizationalUnitRepository = organizationalUnitRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<OrganizationalUnitDto>?>> GetAllOrganizationalUnits(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all OrganizationalUnits.");

        var result = await _organizationalUnitRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No OrganizationalUnits found.", nameof(OrganizationalUnitService), nameof(GetAllOrganizationalUnits));
            return ResponseInfo<List<OrganizationalUnitDto>?>.Success(new List<OrganizationalUnitDto>(), HttpStatusCode.NoContent, "No OrganizationalUnits found.");
        }

        var dtos = _mapper.Map<List<OrganizationalUnitDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} OrganizationalUnits.", nameof(OrganizationalUnitService), nameof(GetAllOrganizationalUnits), dtos.Count);

        return ResponseInfo<List<OrganizationalUnitDto>?>.Success(dtos, HttpStatusCode.OK, "OrganizationalUnits retrieved successfully.");
    }

    public async Task<ResponseInfo<List<OrganizationalUnitDto>?>> GetAllOrganizationalUnits(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all OrganizationalUnits. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _organizationalUnitRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No OrganizationalUnits found.", nameof(OrganizationalUnitService), nameof(GetAllOrganizationalUnits));
            return ResponseInfo<List<OrganizationalUnitDto>?>.Success(new List<OrganizationalUnitDto>(), HttpStatusCode.NoContent, "No OrganizationalUnits found.");
        }

        var dtos = _mapper.Map<List<OrganizationalUnitDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} OrganizationalUnits.", nameof(OrganizationalUnitService), nameof(GetAllOrganizationalUnits), dtos.Count);

        return ResponseInfo<List<OrganizationalUnitDto>?>.Success(dtos, HttpStatusCode.OK, "OrganizationalUnits retrieved successfully.");
    }

    public async Task<ResponseInfo<OrganizationalUnitDto?>> GetOrganizationalUnitById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching OrganizationalUnit Id: {OrganizationalUnitId}", id);

        var result = await _organizationalUnitRepository.GetByIdAsync(id, nameof(OrganizationalUnit.OrganizationalUnitId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalUnit not found Id: {OrganizationalUnitId}.", nameof(OrganizationalUnitService), nameof(GetOrganizationalUnitById), id);
            return ResponseInfo<OrganizationalUnitDto?>.Success(null, HttpStatusCode.NoContent, "OrganizationalUnit not found.");
        }

        var dto = _mapper.Map<OrganizationalUnitDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved OrganizationalUnit Id: {OrganizationalUnitId}.", nameof(OrganizationalUnitService), nameof(GetOrganizationalUnitById), id);

        return ResponseInfo<OrganizationalUnitDto?>.Success(dto, HttpStatusCode.OK, "OrganizationalUnit retrieved successfully.");
    }

    public async Task<ResponseInfo<List<OrganizationalUnitDto>?>> SearchOrganizationalUnit(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching OrganizationalUnits by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _organizationalUnitRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No OrganizationalUnits found.", nameof(OrganizationalUnitService), nameof(SearchOrganizationalUnit));
            return ResponseInfo<List<OrganizationalUnitDto>?>.Success(new List<OrganizationalUnitDto>(), HttpStatusCode.NoContent, "No OrganizationalUnits found.");
        }

        var dtos = _mapper.Map<List<OrganizationalUnitDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} OrganizationalUnits.", nameof(OrganizationalUnitService), nameof(SearchOrganizationalUnit), dtos.Count);

        return ResponseInfo<List<OrganizationalUnitDto>?>.Success(dtos, HttpStatusCode.OK, "OrganizationalUnits retrieved successfully.");
    }

    public async Task<ResponseInfo<OrganizationalUnitDto?>> AddOrganizationalUnit(OrganizationalUnitDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding OrganizationalUnit.");

        var existing = await _organizationalUnitRepository.GetByFieldAsync("OrganizationalUnitCode", dto.OrganizationalUnitCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalUnit already exists with the same OrganizationalUnit Code.", nameof(OrganizationalUnitService), nameof(AddOrganizationalUnit));
            return ResponseInfo<OrganizationalUnitDto?>.Failure("OrganizationalUnit already exists with the same OrganizationalUnit Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<OrganizationalUnit>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _organizationalUnitRepository.AddAsync(entity);

        var resultDto = _mapper.Map<OrganizationalUnitDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalUnit added successfully OrganizationalUnitId: {OrganizationalUnitId}.", nameof(OrganizationalUnitService), nameof(AddOrganizationalUnit), result.OrganizationalUnitId);

        return ResponseInfo<OrganizationalUnitDto?>.Success(resultDto, HttpStatusCode.Created, "OrganizationalUnit added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateOrganizationalUnit(OrganizationalUnitDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating OrganizationalUnit Id: {OrganizationalUnitId}", dto.OrganizationalUnitId);

        var isExists = await _organizationalUnitRepository.IsExistByIdAsync(dto.OrganizationalUnitId, nameof(OrganizationalUnit.OrganizationalUnitId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalUnit not found Id: {OrganizationalUnitId}.", nameof(OrganizationalUnitService), nameof(UpdateOrganizationalUnit), dto.OrganizationalUnitId);
            return ResponseInfo<bool>.Failure("OrganizationalUnit not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<OrganizationalUnit>(dto);
        entity.UpdatedBy = _currentUserService.UserId;
        entity.UpdatedOn = DateTime.UtcNow;

        await _organizationalUnitRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalUnit updated Id: {OrganizationalUnitId}.", nameof(OrganizationalUnitService), nameof(UpdateOrganizationalUnit), dto.OrganizationalUnitId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "OrganizationalUnit updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteOrganizationalUnit(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting OrganizationalUnit Id: {OrganizationalUnitId}", id);

        var isExists = await _organizationalUnitRepository.IsExistByIdAsync(id, nameof(OrganizationalUnit.OrganizationalUnitId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalUnit not found Id: {OrganizationalUnitId}.", nameof(OrganizationalUnitService), nameof(DeleteOrganizationalUnit), id);
            return ResponseInfo<bool>.Failure("OrganizationalUnit not found.", HttpStatusCode.NotFound);
        }

        await _organizationalUnitRepository.DeleteAsync(id, nameof(OrganizationalUnit.OrganizationalUnitId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalUnit deleted Id: {OrganizationalUnitId}.", nameof(OrganizationalUnitService), nameof(DeleteOrganizationalUnit), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "OrganizationalUnit deleted successfully.");
    }
}