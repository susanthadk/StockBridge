using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.OrganizationalLevels;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class OrganizationalLevelService : IOrganizationalLevelService
{
    private readonly IRepository<OrganizationalLevel> _organizationalLevelRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<OrganizationalLevelService> _logger;

    public OrganizationalLevelService(
        IRepository<OrganizationalLevel> organizationalLevelRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<OrganizationalLevelService> logger)
    {
        _organizationalLevelRepository = organizationalLevelRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<OrganizationalLevelDto>?>> GetAllOrganizationalLevels(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all OrganizationalLevels.");

        var result = await _organizationalLevelRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No OrganizationalLevels found.", nameof(OrganizationalLevelService), nameof(GetAllOrganizationalLevels));
            return ResponseInfo<List<OrganizationalLevelDto>?>.Success(new List<OrganizationalLevelDto>(), HttpStatusCode.NoContent, "No OrganizationalLevels found.");
        }

        var dtos = _mapper.Map<List<OrganizationalLevelDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} OrganizationalLevels.", nameof(OrganizationalLevelService), nameof(GetAllOrganizationalLevels), dtos.Count);

        return ResponseInfo<List<OrganizationalLevelDto>?>.Success(dtos, HttpStatusCode.OK, "OrganizationalLevels retrieved successfully.");
    }

    public async Task<ResponseInfo<List<OrganizationalLevelDto>?>> GetAllOrganizationalLevels(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all OrganizationalLevels. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _organizationalLevelRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No OrganizationalLevels found.", nameof(OrganizationalLevelService), nameof(GetAllOrganizationalLevels));
            return ResponseInfo<List<OrganizationalLevelDto>?>.Success(new List<OrganizationalLevelDto>(), HttpStatusCode.NoContent, "No OrganizationalLevels found.");
        }

        var dtos = _mapper.Map<List<OrganizationalLevelDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} OrganizationalLevels.", nameof(OrganizationalLevelService), nameof(GetAllOrganizationalLevels), dtos.Count);

        return ResponseInfo<List<OrganizationalLevelDto>?>.Success(dtos, HttpStatusCode.OK, "OrganizationalLevels retrieved successfully.");
    }

    public async Task<ResponseInfo<OrganizationalLevelDto?>> GetOrganizationalLevelById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching OrganizationalLevel Id: {LevelId}", id);

        var result = await _organizationalLevelRepository.GetByIdAsync(id, nameof(OrganizationalLevel.LevelId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalLevel not found Id: {LevelId}.", nameof(OrganizationalLevelService), nameof(GetOrganizationalLevelById), id);
            return ResponseInfo<OrganizationalLevelDto?>.Success(null, HttpStatusCode.NoContent, "OrganizationalLevel not found.");
        }

        var dto = _mapper.Map<OrganizationalLevelDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved OrganizationalLevel Id: {LevelId}.", nameof(OrganizationalLevelService), nameof(GetOrganizationalLevelById), id);

        return ResponseInfo<OrganizationalLevelDto?>.Success(dto, HttpStatusCode.OK, "OrganizationalLevel retrieved successfully.");
    }

    public async Task<ResponseInfo<List<OrganizationalLevelDto>?>> SearchOrganizationalLevel(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching OrganizationalLevels by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _organizationalLevelRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No OrganizationalLevels found.", nameof(OrganizationalLevelService), nameof(SearchOrganizationalLevel));
            return ResponseInfo<List<OrganizationalLevelDto>?>.Success(new List<OrganizationalLevelDto>(), HttpStatusCode.NoContent, "No OrganizationalLevels found.");
        }

        var dtos = _mapper.Map<List<OrganizationalLevelDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} OrganizationalLevels.", nameof(OrganizationalLevelService), nameof(SearchOrganizationalLevel), dtos.Count);

        return ResponseInfo<List<OrganizationalLevelDto>?>.Success(dtos, HttpStatusCode.OK, "OrganizationalLevels retrieved successfully.");
    }

    public async Task<ResponseInfo<OrganizationalLevelDto?>> AddOrganizationalLevel(OrganizationalLevelDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding OrganizationalLevel.");

        var existing = await _organizationalLevelRepository.GetByFieldAsync("OrganizationLevel", dto.OrganizationLevel);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalLevel already exists with the same OrganizationLevel.", nameof(OrganizationalLevelService), nameof(AddOrganizationalLevel));
            return ResponseInfo<OrganizationalLevelDto?>.Failure("OrganizationalLevel already exists with the same OrganizationLevel.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<OrganizationalLevel>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _organizationalLevelRepository.AddAsync(entity);

        var resultDto = _mapper.Map<OrganizationalLevelDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalLevel added successfully LevelId: {LevelId}.", nameof(OrganizationalLevelService), nameof(AddOrganizationalLevel), result.LevelId);

        return ResponseInfo<OrganizationalLevelDto?>.Success(resultDto, HttpStatusCode.Created, "OrganizationalLevel added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateOrganizationalLevel(OrganizationalLevelDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating OrganizationalLevel Id: {LevelId}", dto.LevelId);

        var isExists = await _organizationalLevelRepository.IsExistByIdAsync(dto.LevelId, nameof(OrganizationalLevel.LevelId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalLevel not found Id: {LevelId}.", nameof(OrganizationalLevelService), nameof(UpdateOrganizationalLevel), dto.LevelId);
            return ResponseInfo<bool>.Failure("OrganizationalLevel not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<OrganizationalLevel>(dto);
        entity.UpdatedBy = _currentUserService.UserId;
        entity.UpdatedOn = DateTime.UtcNow;

        await _organizationalLevelRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalLevel updated Id: {LevelId}.", nameof(OrganizationalLevelService), nameof(UpdateOrganizationalLevel), dto.LevelId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "OrganizationalLevel updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteOrganizationalLevel(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting OrganizationalLevel Id: {LevelId}", id);

        var isExists = await _organizationalLevelRepository.IsExistByIdAsync(id, nameof(OrganizationalLevel.LevelId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalLevel not found Id: {LevelId}.", nameof(OrganizationalLevelService), nameof(DeleteOrganizationalLevel), id);
            return ResponseInfo<bool>.Failure("OrganizationalLevel not found.", HttpStatusCode.NotFound);
        }

        await _organizationalLevelRepository.DeleteAsync(id, nameof(OrganizationalLevel.LevelId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: OrganizationalLevel deleted Id: {LevelId}.", nameof(OrganizationalLevelService), nameof(DeleteOrganizationalLevel), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "OrganizationalLevel deleted successfully.");
    }
}