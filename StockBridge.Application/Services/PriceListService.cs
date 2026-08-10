using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.PriceLists;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class PriceListService : IPriceListService
{
    private readonly IRepository<PriceList> _priceListRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<PriceListService> _logger;

    public PriceListService(
        IRepository<PriceList> priceListRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<PriceListService> logger)
    {
        _priceListRepository = priceListRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<PriceListDto>?>> GetAllPriceLists(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all PriceLists.");

        var result = await _priceListRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No PriceLists found.", nameof(PriceListService), nameof(GetAllPriceLists));
            return ResponseInfo<List<PriceListDto>?>.Success(new List<PriceListDto>(), HttpStatusCode.NoContent, "No PriceLists found.");
        }

        var dtos = _mapper.Map<List<PriceListDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} PriceLists.", nameof(PriceListService), nameof(GetAllPriceLists), dtos.Count);

        return ResponseInfo<List<PriceListDto>?>.Success(dtos, HttpStatusCode.OK, "PriceLists retrieved successfully.");
    }

    public async Task<ResponseInfo<List<PriceListDto>?>> GetAllPriceLists(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all PriceLists. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _priceListRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No PriceLists found.", nameof(PriceListService), nameof(GetAllPriceLists));
            return ResponseInfo<List<PriceListDto>?>.Success(new List<PriceListDto>(), HttpStatusCode.NoContent, "No PriceLists found.");
        }

        var dtos = _mapper.Map<List<PriceListDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} PriceLists.", nameof(PriceListService), nameof(GetAllPriceLists), dtos.Count);

        return ResponseInfo<List<PriceListDto>?>.Success(dtos, HttpStatusCode.OK, "PriceLists retrieved successfully.");
    }

    public async Task<ResponseInfo<PriceListDto?>> GetPriceListById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching PriceList Id: {PriceListId}", id);

        var result = await _priceListRepository.GetByIdAsync(id, nameof(PriceList.PriceListId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: PriceList not found Id: {PriceListId}.", nameof(PriceListService), nameof(GetPriceListById), id);
            return ResponseInfo<PriceListDto?>.Success(null, HttpStatusCode.NoContent, "PriceList not found.");
        }

        var dto = _mapper.Map<PriceListDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved PriceList Id: {PriceListId}.", nameof(PriceListService), nameof(GetPriceListById), id);

        return ResponseInfo<PriceListDto?>.Success(dto, HttpStatusCode.OK, "PriceList retrieved successfully.");
    }

    public async Task<ResponseInfo<List<PriceListDto>?>> SearchPriceList(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching PriceLists by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _priceListRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No PriceLists found.", nameof(PriceListService), nameof(SearchPriceList));
            return ResponseInfo<List<PriceListDto>?>.Success(new List<PriceListDto>(), HttpStatusCode.NoContent, "No PriceLists found.");
        }

        var dtos = _mapper.Map<List<PriceListDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} PriceLists.", nameof(PriceListService), nameof(SearchPriceList), dtos.Count);

        return ResponseInfo<List<PriceListDto>?>.Success(dtos, HttpStatusCode.OK, "PriceLists retrieved successfully.");
    }

    public async Task<ResponseInfo<PriceListDto?>> AddPriceList(PriceListDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding PriceList.");

        var existing = await _priceListRepository.GetByFieldAsync("PriceListPrl", dto.PriceListPrl);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: PriceList already exists with the same Price List PRL.", nameof(PriceListService), nameof(AddPriceList));
            return ResponseInfo<PriceListDto?>.Failure("PriceList already exists with the same Price List PRL.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<PriceList>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _priceListRepository.AddAsync(entity);

        var resultDto = _mapper.Map<PriceListDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: PriceList added successfully PriceListId: {PriceListId}.", nameof(PriceListService), nameof(AddPriceList), result.PriceListId);

        return ResponseInfo<PriceListDto?>.Success(resultDto, HttpStatusCode.Created, "PriceList added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdatePriceList(PriceListDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating PriceList Id: {PriceListId}", dto.PriceListId);

        var isExists = await _priceListRepository.IsExistByIdAsync(dto.PriceListId, nameof(PriceList.PriceListId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: PriceList not found Id: {PriceListId}.", nameof(PriceListService), nameof(UpdatePriceList), dto.PriceListId);
            return ResponseInfo<bool>.Failure("PriceList not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<PriceList>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _priceListRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: PriceList updated Id: {PriceListId}.", nameof(PriceListService), nameof(UpdatePriceList), dto.PriceListId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "PriceList updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeletePriceList(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting PriceList Id: {PriceListId}", id);

        var isExists = await _priceListRepository.IsExistByIdAsync(id, nameof(PriceList.PriceListId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: PriceList not found Id: {PriceListId}.", nameof(PriceListService), nameof(DeletePriceList), id);
            return ResponseInfo<bool>.Failure("PriceList not found.", HttpStatusCode.NotFound);
        }

        await _priceListRepository.DeleteAsync(id, nameof(PriceList.PriceListId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: PriceList deleted Id: {PriceListId}.", nameof(PriceListService), nameof(DeletePriceList), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "PriceList deleted successfully.");
    }
}