using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Items;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class ItemService : IItemService
{
    private readonly IRepository<Item> _itemRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<ItemService> _logger;

    public ItemService(
        IRepository<Item> itemRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<ItemService> logger)
    {
        _itemRepository = itemRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<ItemDto>?>> GetAllItems(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Items.");

        var result = await _itemRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Items found.", nameof(ItemService), nameof(GetAllItems));
            return ResponseInfo<List<ItemDto>?>.Success(new List<ItemDto>(), HttpStatusCode.NoContent, "No Items found.");
        }

        var dtos = _mapper.Map<List<ItemDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Items.", nameof(ItemService), nameof(GetAllItems), dtos.Count);

        return ResponseInfo<List<ItemDto>?>.Success(dtos, HttpStatusCode.OK, "Items retrieved successfully.");
    }

    public async Task<ResponseInfo<List<ItemDto>?>> GetAllItems(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Items. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _itemRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Items found.", nameof(ItemService), nameof(GetAllItems));
            return ResponseInfo<List<ItemDto>?>.Success(new List<ItemDto>(), HttpStatusCode.NoContent, "No Items found.");
        }

        var dtos = _mapper.Map<List<ItemDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Items.", nameof(ItemService), nameof(GetAllItems), dtos.Count);

        return ResponseInfo<List<ItemDto>?>.Success(dtos, HttpStatusCode.OK, "Items retrieved successfully.");
    }

    public async Task<ResponseInfo<ItemDto?>> GetItemById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Item Id: {ItemId}", id);

        var result = await _itemRepository.GetByIdAsync(id, nameof(Item.ItemId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Item not found Id: {ItemId}.", nameof(ItemService), nameof(GetItemById), id);
            return ResponseInfo<ItemDto?>.Success(null, HttpStatusCode.NoContent, "Item not found.");
        }

        var dto = _mapper.Map<ItemDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Item Id: {ItemId}.", nameof(ItemService), nameof(GetItemById), id);

        return ResponseInfo<ItemDto?>.Success(dto, HttpStatusCode.OK, "Item retrieved successfully.");
    }

    public async Task<ResponseInfo<List<ItemDto>?>> SearchItem(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching Items by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _itemRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Items found.", nameof(ItemService), nameof(SearchItem));
            return ResponseInfo<List<ItemDto>?>.Success(new List<ItemDto>(), HttpStatusCode.NoContent, "No Items found.");
        }

        var dtos = _mapper.Map<List<ItemDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Items.", nameof(ItemService), nameof(SearchItem), dtos.Count);

        return ResponseInfo<List<ItemDto>?>.Success(dtos, HttpStatusCode.OK, "Items retrieved successfully.");
    }

    public async Task<ResponseInfo<ItemDto?>> AddItem(ItemDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Item.");

        var existing = await _itemRepository.GetByFieldAsync("ItemCode", dto.ItemCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Item already exists with the same Item Code.", nameof(ItemService), nameof(AddItem));
            return ResponseInfo<ItemDto?>.Failure("Item already exists with the same Item Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Item>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _itemRepository.AddAsync(entity);

        var resultDto = _mapper.Map<ItemDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Item added successfully ItemId: {ItemId}.", nameof(ItemService), nameof(AddItem), result.ItemId);

        return ResponseInfo<ItemDto?>.Success(resultDto, HttpStatusCode.Created, "Item added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateItem(ItemDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Item Id: {ItemId}", dto.ItemId);

        var isExists = await _itemRepository.IsExistByIdAsync(dto.ItemId, nameof(Item.ItemId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Item not found Id: {ItemId}.", nameof(ItemService), nameof(UpdateItem), dto.ItemId);
            return ResponseInfo<bool>.Failure("Item not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<Item>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _itemRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Item updated Id: {ItemId}.", nameof(ItemService), nameof(UpdateItem), dto.ItemId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Item updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteItem(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Item Id: {ItemId}", id);

        var isExists = await _itemRepository.IsExistByIdAsync(id, nameof(Item.ItemId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Item not found Id: {ItemId}.", nameof(ItemService), nameof(DeleteItem), id);
            return ResponseInfo<bool>.Failure("Item not found.", HttpStatusCode.NotFound);
        }

        await _itemRepository.DeleteAsync(id, nameof(Item.ItemId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Item deleted Id: {ItemId}.", nameof(ItemService), nameof(DeleteItem), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Item deleted successfully.");
    }
}