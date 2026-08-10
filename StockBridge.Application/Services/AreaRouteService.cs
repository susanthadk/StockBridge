using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.AreaRoutes;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class AreaRouteService : IAreaRouteService
{
    private readonly IRepository<AreaRoute> _areaRouteRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<AreaRouteService> _logger;

    public AreaRouteService(
        IRepository<AreaRoute> areaRouteRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<AreaRouteService> logger)
    {
        _areaRouteRepository = areaRouteRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<AreaRouteDto>?>> GetAllAreaRoutes(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all AreaRoutes.");

        var result = await _areaRouteRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No AreaRoutes found.", nameof(AreaRouteService), nameof(GetAllAreaRoutes));
            return ResponseInfo<List<AreaRouteDto>?>.Success(new List<AreaRouteDto>(), HttpStatusCode.NoContent, "No AreaRoutes found.");
        }

        var dtos = _mapper.Map<List<AreaRouteDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} AreaRoutes.", nameof(AreaRouteService), nameof(GetAllAreaRoutes), dtos.Count);

        return ResponseInfo<List<AreaRouteDto>?>.Success(dtos, HttpStatusCode.OK, "AreaRoutes retrieved successfully.");
    }

    public async Task<ResponseInfo<List<AreaRouteDto>?>> GetAllAreaRoutes(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all AreaRoutes. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _areaRouteRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No AreaRoutes found.", nameof(AreaRouteService), nameof(GetAllAreaRoutes));
            return ResponseInfo<List<AreaRouteDto>?>.Success(new List<AreaRouteDto>(), HttpStatusCode.NoContent, "No AreaRoutes found.");
        }

        var dtos = _mapper.Map<List<AreaRouteDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} AreaRoutes.", nameof(AreaRouteService), nameof(GetAllAreaRoutes), dtos.Count);

        return ResponseInfo<List<AreaRouteDto>?>.Success(dtos, HttpStatusCode.OK, "AreaRoutes retrieved successfully.");
    }

    public async Task<ResponseInfo<AreaRouteDto?>> GetAreaRouteById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching AreaRoute Id: {AreaRouteId}", id);

        var result = await _areaRouteRepository.GetByIdAsync(id, nameof(AreaRoute.AreaRouteId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: AreaRoute not found Id: {AreaRouteId}.", nameof(AreaRouteService), nameof(GetAreaRouteById), id);
            return ResponseInfo<AreaRouteDto?>.Success(null, HttpStatusCode.NoContent, "AreaRoute not found.");
        }

        var dto = _mapper.Map<AreaRouteDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved AreaRoute Id: {AreaRouteId}.", nameof(AreaRouteService), nameof(GetAreaRouteById), id);

        return ResponseInfo<AreaRouteDto?>.Success(dto, HttpStatusCode.OK, "AreaRoute retrieved successfully.");
    }

    public async Task<ResponseInfo<List<AreaRouteDto>?>> SearchAreaRoute(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching AreaRoutes by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _areaRouteRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No AreaRoutes found.", nameof(AreaRouteService), nameof(SearchAreaRoute));
            return ResponseInfo<List<AreaRouteDto>?>.Success(new List<AreaRouteDto>(), HttpStatusCode.NoContent, "No AreaRoutes found.");
        }

        var dtos = _mapper.Map<List<AreaRouteDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} AreaRoutes.", nameof(AreaRouteService), nameof(SearchAreaRoute), dtos.Count);

        return ResponseInfo<List<AreaRouteDto>?>.Success(dtos, HttpStatusCode.OK, "AreaRoutes retrieved successfully.");
    }

    public async Task<ResponseInfo<AreaRouteDto?>> AddAreaRoute(AreaRouteDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding AreaRoute.");

        var existing = await _areaRouteRepository.GetByFieldAsync("AreaCode", dto.AreaCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: AreaRoute already exists with the same Area Code.", nameof(AreaRouteService), nameof(AddAreaRoute));
            return ResponseInfo<AreaRouteDto?>.Failure("AreaRoute already exists with the same Area Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<AreaRoute>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _areaRouteRepository.AddAsync(entity);

        var resultDto = _mapper.Map<AreaRouteDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: AreaRoute added successfully AreaRouteId: {AreaRouteId}.", nameof(AreaRouteService), nameof(AddAreaRoute), result.AreaRouteId);

        return ResponseInfo<AreaRouteDto?>.Success(resultDto, HttpStatusCode.Created, "AreaRoute added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateAreaRoute(AreaRouteDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating AreaRoute Id: {AreaRouteId}", dto.AreaRouteId);

        var isExists = await _areaRouteRepository.IsExistByIdAsync(dto.AreaRouteId, nameof(AreaRoute.AreaRouteId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: AreaRoute not found Id: {AreaRouteId}.", nameof(AreaRouteService), nameof(UpdateAreaRoute), dto.AreaRouteId);
            return ResponseInfo<bool>.Failure("AreaRoute not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<AreaRoute>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _areaRouteRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: AreaRoute updated Id: {AreaRouteId}.", nameof(AreaRouteService), nameof(UpdateAreaRoute), dto.AreaRouteId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "AreaRoute updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteAreaRoute(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting AreaRoute Id: {AreaRouteId}", id);

        var isExists = await _areaRouteRepository.IsExistByIdAsync(id, nameof(AreaRoute.AreaRouteId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: AreaRoute not found Id: {AreaRouteId}.", nameof(AreaRouteService), nameof(DeleteAreaRoute), id);
            return ResponseInfo<bool>.Failure("AreaRoute not found.", HttpStatusCode.NotFound);
        }

        await _areaRouteRepository.DeleteAsync(id, nameof(AreaRoute.AreaRouteId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: AreaRoute deleted Id: {AreaRouteId}.", nameof(AreaRouteService), nameof(DeleteAreaRoute), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "AreaRoute deleted successfully.");
    }
}