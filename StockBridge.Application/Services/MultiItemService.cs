using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.MultiItems;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class MultiItemService : IMultiItemService
{
    private readonly IRepository<MultiItem> _multiItemRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<MultiItemService> _logger;

    public MultiItemService(
        IRepository<MultiItem> multiItemRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<MultiItemService> logger)
    {
        _multiItemRepository = multiItemRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<MultiItemDto>?>> GetAllMultiItems(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all MultiItems.");

        var result = await _multiItemRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No MultiItems found.", nameof(MultiItemService), nameof(GetAllMultiItems));
            return ResponseInfo<List<MultiItemDto>?>.Success(new List<MultiItemDto>(), HttpStatusCode.NoContent, "No MultiItems found.");
        }

        var dtos = _mapper.Map<List<MultiItemDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} MultiItems.", nameof(MultiItemService), nameof(GetAllMultiItems), dtos.Count);

        return ResponseInfo<List<MultiItemDto>?>.Success(dtos, HttpStatusCode.OK, "MultiItems retrieved successfully.");
    }

    public async Task<ResponseInfo<List<MultiItemDto>?>> GetAllMultiItems(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all MultiItems. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _multiItemRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No MultiItems found.", nameof(MultiItemService), nameof(GetAllMultiItems));
            return ResponseInfo<List<MultiItemDto>?>.Success(new List<MultiItemDto>(), HttpStatusCode.NoContent, "No MultiItems found.");
        }

        var dtos = _mapper.Map<List<MultiItemDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} MultiItems.", nameof(MultiItemService), nameof(GetAllMultiItems), dtos.Count);

        return ResponseInfo<List<MultiItemDto>?>.Success(dtos, HttpStatusCode.OK, "MultiItems retrieved successfully.");
    }

    public async Task<ResponseInfo<MultiItemDto?>> GetMultiItemById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching MultiItem Id: {MultiItemId}", id);

        var result = await _multiItemRepository.GetByIdAsync(id, nameof(MultiItem.MultiItemId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: MultiItem not found Id: {MultiItemId}.", nameof(MultiItemService), nameof(GetMultiItemById), id);
            return ResponseInfo<MultiItemDto?>.Success(null, HttpStatusCode.NoContent, "MultiItem not found.");
        }

        var dto = _mapper.Map<MultiItemDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved MultiItem Id: {MultiItemId}.", nameof(MultiItemService), nameof(GetMultiItemById), id);

        return ResponseInfo<MultiItemDto?>.Success(dto, HttpStatusCode.OK, "MultiItem retrieved successfully.");
    }

    public async Task<ResponseInfo<List<MultiItemDto>?>> SearchMultiItem(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching MultiItems by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _multiItemRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No MultiItems found.", nameof(MultiItemService), nameof(SearchMultiItem));
            return ResponseInfo<List<MultiItemDto>?>.Success(new List<MultiItemDto>(), HttpStatusCode.NoContent, "No MultiItems found.");
        }

        var dtos = _mapper.Map<List<MultiItemDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} MultiItems.", nameof(MultiItemService), nameof(SearchMultiItem), dtos.Count);

        return ResponseInfo<List<MultiItemDto>?>.Success(dtos, HttpStatusCode.OK, "MultiItems retrieved successfully.");
    }

    public async Task<ResponseInfo<MultiItemDto?>> AddMultiItem(MultiItemDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding MultiItem.");

        var existing = await _multiItemRepository.GetByFieldAsync("StockCode", dto.StockCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: MultiItem already exists with the same Stock Code.", nameof(MultiItemService), nameof(AddMultiItem));
            return ResponseInfo<MultiItemDto?>.Failure("MultiItem already exists with the same Stock Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<MultiItem>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _multiItemRepository.AddAsync(entity);

        var resultDto = _mapper.Map<MultiItemDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: MultiItem added successfully MultiItemId: {MultiItemId}.", nameof(MultiItemService), nameof(AddMultiItem), result.MultiItemId);

        return ResponseInfo<MultiItemDto?>.Success(resultDto, HttpStatusCode.Created, "MultiItem added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateMultiItem(MultiItemDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating MultiItem Id: {MultiItemId}", dto.MultiItemId);

        var isExists = await _multiItemRepository.IsExistByIdAsync(dto.MultiItemId, nameof(MultiItem.MultiItemId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: MultiItem not found Id: {MultiItemId}.", nameof(MultiItemService), nameof(UpdateMultiItem), dto.MultiItemId);
            return ResponseInfo<bool>.Failure("MultiItem not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<MultiItem>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _multiItemRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: MultiItem updated Id: {MultiItemId}.", nameof(MultiItemService), nameof(UpdateMultiItem), dto.MultiItemId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "MultiItem updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteMultiItem(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting MultiItem Id: {MultiItemId}", id);

        var isExists = await _multiItemRepository.IsExistByIdAsync(id, nameof(MultiItem.MultiItemId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: MultiItem not found Id: {MultiItemId}.", nameof(MultiItemService), nameof(DeleteMultiItem), id);
            return ResponseInfo<bool>.Failure("MultiItem not found.", HttpStatusCode.NotFound);
        }

        await _multiItemRepository.DeleteAsync(id, nameof(MultiItem.MultiItemId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: MultiItem deleted Id: {MultiItemId}.", nameof(MultiItemService), nameof(DeleteMultiItem), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "MultiItem deleted successfully.");
    }
}