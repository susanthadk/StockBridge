using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.DeliveryMethods;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class DeliveryMethodService : IDeliveryMethodService
{
    private readonly IRepository<DeliveryMethod> _deliveryMethodRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<DeliveryMethodService> _logger;

    public DeliveryMethodService(
        IRepository<DeliveryMethod> deliveryMethodRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<DeliveryMethodService> logger)
    {
        _deliveryMethodRepository = deliveryMethodRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<DeliveryMethodDto>?>> GetAllDeliveryMethods(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all DeliveryMethods.");

        var result = await _deliveryMethodRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No DeliveryMethods found.", nameof(DeliveryMethodService), nameof(GetAllDeliveryMethods));
            return ResponseInfo<List<DeliveryMethodDto>?>.Success(new List<DeliveryMethodDto>(), HttpStatusCode.NoContent, "No DeliveryMethods found.");
        }

        var dtos = _mapper.Map<List<DeliveryMethodDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} DeliveryMethods.", nameof(DeliveryMethodService), nameof(GetAllDeliveryMethods), dtos.Count);

        return ResponseInfo<List<DeliveryMethodDto>?>.Success(dtos, HttpStatusCode.OK, "DeliveryMethods retrieved successfully.");
    }

    public async Task<ResponseInfo<List<DeliveryMethodDto>?>> GetAllDeliveryMethods(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all DeliveryMethods. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _deliveryMethodRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No DeliveryMethods found.", nameof(DeliveryMethodService), nameof(GetAllDeliveryMethods));
            return ResponseInfo<List<DeliveryMethodDto>?>.Success(new List<DeliveryMethodDto>(), HttpStatusCode.NoContent, "No DeliveryMethods found.");
        }

        var dtos = _mapper.Map<List<DeliveryMethodDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} DeliveryMethods.", nameof(DeliveryMethodService), nameof(GetAllDeliveryMethods), dtos.Count);

        return ResponseInfo<List<DeliveryMethodDto>?>.Success(dtos, HttpStatusCode.OK, "DeliveryMethods retrieved successfully.");
    }

    public async Task<ResponseInfo<DeliveryMethodDto?>> GetDeliveryMethodById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching DeliveryMethod Id: {DeliveryMethodId}", id);

        var result = await _deliveryMethodRepository.GetByIdAsync(id, nameof(DeliveryMethod.DeliveryMethodId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: DeliveryMethod not found Id: {DeliveryMethodId}.", nameof(DeliveryMethodService), nameof(GetDeliveryMethodById), id);
            return ResponseInfo<DeliveryMethodDto?>.Success(null, HttpStatusCode.NoContent, "DeliveryMethod not found.");
        }

        var dto = _mapper.Map<DeliveryMethodDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved DeliveryMethod Id: {DeliveryMethodId}.", nameof(DeliveryMethodService), nameof(GetDeliveryMethodById), id);

        return ResponseInfo<DeliveryMethodDto?>.Success(dto, HttpStatusCode.OK, "DeliveryMethod retrieved successfully.");
    }

    public async Task<ResponseInfo<List<DeliveryMethodDto>?>> SearchDeliveryMethod(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching DeliveryMethods by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _deliveryMethodRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No DeliveryMethods found.", nameof(DeliveryMethodService), nameof(SearchDeliveryMethod));
            return ResponseInfo<List<DeliveryMethodDto>?>.Success(new List<DeliveryMethodDto>(), HttpStatusCode.NoContent, "No DeliveryMethods found.");
        }

        var dtos = _mapper.Map<List<DeliveryMethodDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} DeliveryMethods.", nameof(DeliveryMethodService), nameof(SearchDeliveryMethod), dtos.Count);

        return ResponseInfo<List<DeliveryMethodDto>?>.Success(dtos, HttpStatusCode.OK, "DeliveryMethods retrieved successfully.");
    }

    public async Task<ResponseInfo<DeliveryMethodDto?>> AddDeliveryMethod(DeliveryMethodDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding DeliveryMethod.");

        var existing = await _deliveryMethodRepository.GetByFieldAsync("DeliveryMethodCode", dto.DeliveryMethodCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: DeliveryMethod already exists with the same DeliveryMethod Code.", nameof(DeliveryMethodService), nameof(AddDeliveryMethod));
            return ResponseInfo<DeliveryMethodDto?>.Failure("DeliveryMethod already exists with the same DeliveryMethod Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<DeliveryMethod>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _deliveryMethodRepository.AddAsync(entity);

        var resultDto = _mapper.Map<DeliveryMethodDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: DeliveryMethod added successfully DeliveryMethodId: {DeliveryMethodId}.", nameof(DeliveryMethodService), nameof(AddDeliveryMethod), result.DeliveryMethodId);

        return ResponseInfo<DeliveryMethodDto?>.Success(resultDto, HttpStatusCode.Created, "DeliveryMethod added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateDeliveryMethod(DeliveryMethodDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating DeliveryMethod Id: {DeliveryMethodId}", dto.DeliveryMethodId);

        var isExists = await _deliveryMethodRepository.IsExistByIdAsync(dto.DeliveryMethodId, nameof(DeliveryMethod.DeliveryMethodId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: DeliveryMethod not found Id: {DeliveryMethodId}.", nameof(DeliveryMethodService), nameof(UpdateDeliveryMethod), dto.DeliveryMethodId);
            return ResponseInfo<bool>.Failure("DeliveryMethod not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<DeliveryMethod>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _deliveryMethodRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: DeliveryMethod updated Id: {DeliveryMethodId}.", nameof(DeliveryMethodService), nameof(UpdateDeliveryMethod), dto.DeliveryMethodId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "DeliveryMethod updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteDeliveryMethod(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting DeliveryMethod Id: {DeliveryMethodId}", id);

        var isExists = await _deliveryMethodRepository.IsExistByIdAsync(id, nameof(DeliveryMethod.DeliveryMethodId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: DeliveryMethod not found Id: {DeliveryMethodId}.", nameof(DeliveryMethodService), nameof(DeleteDeliveryMethod), id);
            return ResponseInfo<bool>.Failure("DeliveryMethod not found.", HttpStatusCode.NotFound);
        }

        await _deliveryMethodRepository.DeleteAsync(id, nameof(DeliveryMethod.DeliveryMethodId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: DeliveryMethod deleted Id: {DeliveryMethodId}.", nameof(DeliveryMethodService), nameof(DeleteDeliveryMethod), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "DeliveryMethod deleted successfully.");
    }
}
