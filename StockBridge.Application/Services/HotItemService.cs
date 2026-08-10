using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.HotItems;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class HotItemService : IHotItemService
{
    private readonly IRepository<HotItem> _hotItemRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<HotItemService> _logger;

    public HotItemService(
        IRepository<HotItem> hotItemRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<HotItemService> logger)
    {
        _hotItemRepository = hotItemRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<HotItemDto>?>> GetAllHotItems(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all HotItems.");

        var result = await _hotItemRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No HotItems found.", nameof(HotItemService), nameof(GetAllHotItems));
            return ResponseInfo<List<HotItemDto>?>.Success(new List<HotItemDto>(), HttpStatusCode.NoContent, "No HotItems found.");
        }

        var dtos = _mapper.Map<List<HotItemDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} HotItems.", nameof(HotItemService), nameof(GetAllHotItems), dtos.Count);

        return ResponseInfo<List<HotItemDto>?>.Success(dtos, HttpStatusCode.OK, "HotItems retrieved successfully.");
    }

    public async Task<ResponseInfo<List<HotItemDto>?>> GetAllHotItems(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all HotItems. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _hotItemRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No HotItems found.", nameof(HotItemService), nameof(GetAllHotItems));
            return ResponseInfo<List<HotItemDto>?>.Success(new List<HotItemDto>(), HttpStatusCode.NoContent, "No HotItems found.");
        }

        var dtos = _mapper.Map<List<HotItemDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} HotItems.", nameof(HotItemService), nameof(GetAllHotItems), dtos.Count);

        return ResponseInfo<List<HotItemDto>?>.Success(dtos, HttpStatusCode.OK, "HotItems retrieved successfully.");
    }

    public async Task<ResponseInfo<HotItemDto?>> GetHotItemById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching HotItem Id: {HotItemId}", id);

        var result = await _hotItemRepository.GetByIdAsync(id, nameof(HotItem.HotItemId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: HotItem not found Id: {HotItemId}.", nameof(HotItemService), nameof(GetHotItemById), id);
            return ResponseInfo<HotItemDto?>.Success(null, HttpStatusCode.NoContent, "HotItem not found.");
        }

        var dto = _mapper.Map<HotItemDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved HotItem Id: {HotItemId}.", nameof(HotItemService), nameof(GetHotItemById), id);

        return ResponseInfo<HotItemDto?>.Success(dto, HttpStatusCode.OK, "HotItem retrieved successfully.");
    }

    public async Task<ResponseInfo<List<HotItemDto>?>> SearchHotItem(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching HotItems by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _hotItemRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No HotItems found.", nameof(HotItemService), nameof(SearchHotItem));
            return ResponseInfo<List<HotItemDto>?>.Success(new List<HotItemDto>(), HttpStatusCode.NoContent, "No HotItems found.");
        }

        var dtos = _mapper.Map<List<HotItemDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} HotItems.", nameof(HotItemService), nameof(SearchHotItem), dtos.Count);

        return ResponseInfo<List<HotItemDto>?>.Success(dtos, HttpStatusCode.OK, "HotItems retrieved successfully.");
    }

    public async Task<ResponseInfo<HotItemDto?>> AddHotItem(HotItemDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding HotItem.");

        var entity = _mapper.Map<HotItem>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _hotItemRepository.AddAsync(entity);

        var resultDto = _mapper.Map<HotItemDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: HotItem added successfully HotItemId: {HotItemId}.", nameof(HotItemService), nameof(AddHotItem), result.HotItemId);

        return ResponseInfo<HotItemDto?>.Success(resultDto, HttpStatusCode.Created, "HotItem added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateHotItem(HotItemDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating HotItem Id: {HotItemId}", dto.HotItemId);

        var isExists = await _hotItemRepository.IsExistByIdAsync(dto.HotItemId, nameof(HotItem.HotItemId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: HotItem not found Id: {HotItemId}.", nameof(HotItemService), nameof(UpdateHotItem), dto.HotItemId);
            return ResponseInfo<bool>.Failure("HotItem not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<HotItem>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _hotItemRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: HotItem updated Id: {HotItemId}.", nameof(HotItemService), nameof(UpdateHotItem), dto.HotItemId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "HotItem updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteHotItem(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting HotItem Id: {HotItemId}", id);

        var isExists = await _hotItemRepository.IsExistByIdAsync(id, nameof(HotItem.HotItemId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: HotItem not found Id: {HotItemId}.", nameof(HotItemService), nameof(DeleteHotItem), id);
            return ResponseInfo<bool>.Failure("HotItem not found.", HttpStatusCode.NotFound);
        }

        await _hotItemRepository.DeleteAsync(id, nameof(HotItem.HotItemId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: HotItem deleted Id: {HotItemId}.", nameof(HotItemService), nameof(DeleteHotItem), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "HotItem deleted successfully.");
    }
}