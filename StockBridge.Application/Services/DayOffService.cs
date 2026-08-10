using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.DayOffs;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class DayOffService : IDayOffService
{
    private readonly IRepository<DayOff> _dayOffRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<DayOffService> _logger;

    public DayOffService(
        IRepository<DayOff> dayOffRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<DayOffService> logger)
    {
        _dayOffRepository = dayOffRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<DayOffDto>?>> GetAllDayOffs(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all DayOffs.");

        var result = await _dayOffRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No DayOffs found.", nameof(DayOffService), nameof(GetAllDayOffs));
            return ResponseInfo<List<DayOffDto>?>.Success(new List<DayOffDto>(), HttpStatusCode.NoContent, "No DayOffs found.");
        }

        var dtos = _mapper.Map<List<DayOffDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} DayOffs.", nameof(DayOffService), nameof(GetAllDayOffs), dtos.Count);

        return ResponseInfo<List<DayOffDto>?>.Success(dtos, HttpStatusCode.OK, "DayOffs retrieved successfully.");
    }

    public async Task<ResponseInfo<List<DayOffDto>?>> GetAllDayOffs(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all DayOffs. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _dayOffRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No DayOffs found.", nameof(DayOffService), nameof(GetAllDayOffs));
            return ResponseInfo<List<DayOffDto>?>.Success(new List<DayOffDto>(), HttpStatusCode.NoContent, "No DayOffs found.");
        }

        var dtos = _mapper.Map<List<DayOffDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} DayOffs.", nameof(DayOffService), nameof(GetAllDayOffs), dtos.Count);

        return ResponseInfo<List<DayOffDto>?>.Success(dtos, HttpStatusCode.OK, "DayOffs retrieved successfully.");
    }

    public async Task<ResponseInfo<DayOffDto?>> GetDayOffById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching DayOff Id: {DayOffId}", id);

        var result = await _dayOffRepository.GetByIdAsync(id, nameof(DayOff.DayOffId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: DayOff not found Id: {DayOffId}.", nameof(DayOffService), nameof(GetDayOffById), id);
            return ResponseInfo<DayOffDto?>.Success(null, HttpStatusCode.NoContent, "DayOff not found.");
        }

        var dto = _mapper.Map<DayOffDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved DayOff Id: {DayOffId}.", nameof(DayOffService), nameof(GetDayOffById), id);

        return ResponseInfo<DayOffDto?>.Success(dto, HttpStatusCode.OK, "DayOff retrieved successfully.");
    }

    public async Task<ResponseInfo<List<DayOffDto>?>> SearchDayOff(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching DayOffs by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _dayOffRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No DayOffs found.", nameof(DayOffService), nameof(SearchDayOff));
            return ResponseInfo<List<DayOffDto>?>.Success(new List<DayOffDto>(), HttpStatusCode.NoContent, "No DayOffs found.");
        }

        var dtos = _mapper.Map<List<DayOffDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} DayOffs.", nameof(DayOffService), nameof(SearchDayOff), dtos.Count);

        return ResponseInfo<List<DayOffDto>?>.Success(dtos, HttpStatusCode.OK, "DayOffs retrieved successfully.");
    }

    public async Task<ResponseInfo<DayOffDto?>> AddDayOff(DayOffDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding DayOff.");

        var entity = _mapper.Map<DayOff>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _dayOffRepository.AddAsync(entity);

        var resultDto = _mapper.Map<DayOffDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: DayOff added successfully DayOffId: {DayOffId}.", nameof(DayOffService), nameof(AddDayOff), result.DayOffId);

        return ResponseInfo<DayOffDto?>.Success(resultDto, HttpStatusCode.Created, "DayOff added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateDayOff(DayOffDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating DayOff Id: {DayOffId}", dto.DayOffId);

        var isExists = await _dayOffRepository.IsExistByIdAsync(dto.DayOffId, nameof(DayOff.DayOffId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: DayOff not found Id: {DayOffId}.", nameof(DayOffService), nameof(UpdateDayOff), dto.DayOffId);
            return ResponseInfo<bool>.Failure("DayOff not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<DayOff>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _dayOffRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: DayOff updated Id: {DayOffId}.", nameof(DayOffService), nameof(UpdateDayOff), dto.DayOffId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "DayOff updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteDayOff(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting DayOff Id: {DayOffId}", id);

        var isExists = await _dayOffRepository.IsExistByIdAsync(id, nameof(DayOff.DayOffId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: DayOff not found Id: {DayOffId}.", nameof(DayOffService), nameof(DeleteDayOff), id);
            return ResponseInfo<bool>.Failure("DayOff not found.", HttpStatusCode.NotFound);
        }

        await _dayOffRepository.DeleteAsync(id, nameof(DayOff.DayOffId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: DayOff deleted Id: {DayOffId}.", nameof(DayOffService), nameof(DeleteDayOff), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "DayOff deleted successfully.");
    }
}