using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.MainLocations;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class MainLocationService : IMainLocationService
{
    private readonly IRepository<MainLocation> _mainLocationRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<MainLocationService> _logger;

    public MainLocationService(
        IRepository<MainLocation> mainLocationRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<MainLocationService> logger)
    {
        _mainLocationRepository = mainLocationRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<MainLocationDto>?>> GetAllMainLocations(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all MainLocations.");

        var result = await _mainLocationRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No MainLocations found.", nameof(MainLocationService), nameof(GetAllMainLocations));
            return ResponseInfo<List<MainLocationDto>?>.Success(new List<MainLocationDto>(), HttpStatusCode.NoContent, "No MainLocations found.");
        }

        var dtos = _mapper.Map<List<MainLocationDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} MainLocations.", nameof(MainLocationService), nameof(GetAllMainLocations), dtos.Count);

        return ResponseInfo<List<MainLocationDto>?>.Success(dtos, HttpStatusCode.OK, "MainLocations retrieved successfully.");
    }

    public async Task<ResponseInfo<List<MainLocationDto>?>> GetAllMainLocations(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all MainLocations. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _mainLocationRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No MainLocations found.", nameof(MainLocationService), nameof(GetAllMainLocations));
            return ResponseInfo<List<MainLocationDto>?>.Success(new List<MainLocationDto>(), HttpStatusCode.NoContent, "No MainLocations found.");
        }

        var dtos = _mapper.Map<List<MainLocationDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} MainLocations.", nameof(MainLocationService), nameof(GetAllMainLocations), dtos.Count);

        return ResponseInfo<List<MainLocationDto>?>.Success(dtos, HttpStatusCode.OK, "MainLocations retrieved successfully.");
    }

    public async Task<ResponseInfo<MainLocationDto?>> GetMainLocationById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching MainLocation Id: {MainLocationId}", id);

        var result = await _mainLocationRepository.GetByIdAsync(id, nameof(MainLocation.MainLocationId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: MainLocation not found Id: {MainLocationId}.", nameof(MainLocationService), nameof(GetMainLocationById), id);
            return ResponseInfo<MainLocationDto?>.Success(null, HttpStatusCode.NoContent, "MainLocation not found.");
        }

        var dto = _mapper.Map<MainLocationDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved MainLocation Id: {MainLocationId}.", nameof(MainLocationService), nameof(GetMainLocationById), id);

        return ResponseInfo<MainLocationDto?>.Success(dto, HttpStatusCode.OK, "MainLocation retrieved successfully.");
    }

    public async Task<ResponseInfo<List<MainLocationDto>?>> SearchMainLocation(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching MainLocations by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _mainLocationRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No MainLocations found.", nameof(MainLocationService), nameof(SearchMainLocation));
            return ResponseInfo<List<MainLocationDto>?>.Success(new List<MainLocationDto>(), HttpStatusCode.NoContent, "No MainLocations found.");
        }

        var dtos = _mapper.Map<List<MainLocationDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} MainLocations.", nameof(MainLocationService), nameof(SearchMainLocation), dtos.Count);

        return ResponseInfo<List<MainLocationDto>?>.Success(dtos, HttpStatusCode.OK, "MainLocations retrieved successfully.");
    }

    public async Task<ResponseInfo<MainLocationDto?>> AddMainLocation(MainLocationDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding MainLocation.");

        var existing = await _mainLocationRepository.GetByFieldAsync("MainLocCode", dto.MainLocCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: MainLocation already exists with the same Main Location Code.", nameof(MainLocationService), nameof(AddMainLocation));
            return ResponseInfo<MainLocationDto?>.Failure("MainLocation already exists with the same Main Location Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<MainLocation>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _mainLocationRepository.AddAsync(entity);

        var resultDto = _mapper.Map<MainLocationDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: MainLocation added successfully MainLocationId: {MainLocationId}.", nameof(MainLocationService), nameof(AddMainLocation), result.MainLocationId);

        return ResponseInfo<MainLocationDto?>.Success(resultDto, HttpStatusCode.Created, "MainLocation added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateMainLocation(MainLocationDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating MainLocation Id: {MainLocationId}", dto.MainLocationId);

        var isExists = await _mainLocationRepository.IsExistByIdAsync(dto.MainLocationId, nameof(MainLocation.MainLocationId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: MainLocation not found Id: {MainLocationId}.", nameof(MainLocationService), nameof(UpdateMainLocation), dto.MainLocationId);
            return ResponseInfo<bool>.Failure("MainLocation not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<MainLocation>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _mainLocationRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: MainLocation updated Id: {MainLocationId}.", nameof(MainLocationService), nameof(UpdateMainLocation), dto.MainLocationId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "MainLocation updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteMainLocation(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting MainLocation Id: {MainLocationId}", id);

        var isExists = await _mainLocationRepository.IsExistByIdAsync(id, nameof(MainLocation.MainLocationId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: MainLocation not found Id: {MainLocationId}.", nameof(MainLocationService), nameof(DeleteMainLocation), id);
            return ResponseInfo<bool>.Failure("MainLocation not found.", HttpStatusCode.NotFound);
        }

        await _mainLocationRepository.DeleteAsync(id, nameof(MainLocation.MainLocationId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: MainLocation deleted Id: {MainLocationId}.", nameof(MainLocationService), nameof(DeleteMainLocation), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "MainLocation deleted successfully.");
    }
}