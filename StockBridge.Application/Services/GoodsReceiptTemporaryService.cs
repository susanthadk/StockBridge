using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.GoodsReceiptTemporaries;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class GoodsReceiptTemporaryService : IGoodsReceiptTemporaryService
{
    private readonly IGoodsReceiptTemporaryRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GoodsReceiptTemporaryService> _logger;

    public GoodsReceiptTemporaryService(
        IGoodsReceiptTemporaryRepository repository,
        IMapper mapper,
        ILogger<GoodsReceiptTemporaryService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<GoodsReceiptTemporaryHeaderDto>?>> GetAllGoodsReceiptTemporaries(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all GoodsReceiptTemporaries.");

        var result = await _repository.GetAllWithDetailsAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No GoodsReceiptTemporaries found.", nameof(GoodsReceiptTemporaryService), nameof(GetAllGoodsReceiptTemporaries));
            return ResponseInfo<List<GoodsReceiptTemporaryHeaderDto>?>.Success(new List<GoodsReceiptTemporaryHeaderDto>(), HttpStatusCode.NoContent, "No GoodsReceiptTemporaries found.");
        }

        var dtos = _mapper.Map<List<GoodsReceiptTemporaryHeaderDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} GoodsReceiptTemporaries.", nameof(GoodsReceiptTemporaryService), nameof(GetAllGoodsReceiptTemporaries), dtos.Count);

        return ResponseInfo<List<GoodsReceiptTemporaryHeaderDto>?>.Success(dtos, HttpStatusCode.OK, "GoodsReceiptTemporaries retrieved successfully.");
    }

    public async Task<ResponseInfo<GoodsReceiptTemporaryHeaderDto?>> GetGoodsReceiptTemporaryById(long headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching GoodsReceiptTemporary Id: {HeaderId}", headerId);

        var result = await _repository.GetByIdWithDetailsAsync(headerId);

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: GoodsReceiptTemporary not found Id: {HeaderId}.", nameof(GoodsReceiptTemporaryService), nameof(GetGoodsReceiptTemporaryById), headerId);
            return ResponseInfo<GoodsReceiptTemporaryHeaderDto?>.Success(null, HttpStatusCode.NoContent, "GoodsReceiptTemporary not found.");
        }

        var dto = _mapper.Map<GoodsReceiptTemporaryHeaderDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved GoodsReceiptTemporary Id: {HeaderId}.", nameof(GoodsReceiptTemporaryService), nameof(GetGoodsReceiptTemporaryById), headerId);

        return ResponseInfo<GoodsReceiptTemporaryHeaderDto?>.Success(dto, HttpStatusCode.OK, "GoodsReceiptTemporary retrieved successfully.");
    }

    public async Task<ResponseInfo<GoodsReceiptTemporaryHeaderDto?>> AddGoodsReceiptTemporary(CreateGoodsReceiptTemporaryHeaderDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding GoodsReceiptTemporary.");

        if (dto.GoodsReceiptTemporaryDetails == null || !dto.GoodsReceiptTemporaryDetails.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: GoodsReceiptTemporary cannot be added without details.", nameof(GoodsReceiptTemporaryService), nameof(AddGoodsReceiptTemporary));
            return ResponseInfo<GoodsReceiptTemporaryHeaderDto?>.Failure("GoodsReceiptTemporary must contain at least one detail line.", HttpStatusCode.BadRequest);
        }

        var existing = await _repository.GetByFieldAsync("GoodsReceiptNumber", dto.GoodsReceiptNumber);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: GoodsReceiptTemporary already exists with the same Goods Receipt Number.", nameof(GoodsReceiptTemporaryService), nameof(AddGoodsReceiptTemporary));
            return ResponseInfo<GoodsReceiptTemporaryHeaderDto?>.Failure("GoodsReceiptTemporary already exists with the same Goods Receipt Number.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<GoodsReceiptTemporaryHeader>(dto);

        await _repository.AddAsync(entity);

        var result = _mapper.Map<GoodsReceiptTemporaryHeaderDto>(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: GoodsReceiptTemporary added successfully HeaderId: {HeaderId}.", nameof(GoodsReceiptTemporaryService), nameof(AddGoodsReceiptTemporary), entity.GoodsReceiptTemporaryHeaderId);

        return ResponseInfo<GoodsReceiptTemporaryHeaderDto?>.Success(result, HttpStatusCode.OK, "GoodsReceiptTemporary added successfully.");
    }

    public async Task<ResponseInfo<GoodsReceiptTemporaryHeaderDto?>> UpdateGoodsReceiptTemporary(UpdateGoodsReceiptTemporaryHeaderDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating GoodsReceiptTemporary Id: {HeaderId}", dto.GoodsReceiptTemporaryHeaderId);

        var entity = await _repository.GetByIdWithDetailsAsync(dto.GoodsReceiptTemporaryHeaderId);

        if (entity == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: GoodsReceiptTemporary not found Id: {HeaderId}.", nameof(GoodsReceiptTemporaryService), nameof(UpdateGoodsReceiptTemporary), dto.GoodsReceiptTemporaryHeaderId);
            return ResponseInfo<GoodsReceiptTemporaryHeaderDto?>.Failure("GoodsReceiptTemporary not found.", HttpStatusCode.NotFound);
        }

        _mapper.Map(dto, entity);

        var dtoDetailIds = new HashSet<long>(dto.GoodsReceiptTemporaryDetails.Where(d => d.GoodsReceiptTemporaryDetailId > 0).Select(d => d.GoodsReceiptTemporaryDetailId));

        foreach (var detailDto in dto.GoodsReceiptTemporaryDetails)
        {
            if (detailDto.GoodsReceiptTemporaryDetailId > 0)
            {
                var existingDetail = entity.GoodsReceiptTemporaryDetails.FirstOrDefault(d => d.GoodsReceiptTemporaryDetailId == detailDto.GoodsReceiptTemporaryDetailId);

                if (existingDetail != null)
                {
                    _mapper.Map(detailDto, existingDetail);
                    continue;
                }
            }

            var newDetail = _mapper.Map<GoodsReceiptTemporaryDetail>(detailDto);
            entity.GoodsReceiptTemporaryDetails.Add(newDetail);
        }

        foreach (var removedDetail in entity.GoodsReceiptTemporaryDetails.Where(d => !dtoDetailIds.Contains(d.GoodsReceiptTemporaryDetailId)).ToList())
        {
            entity.GoodsReceiptTemporaryDetails.Remove(removedDetail);
        }

        await _repository.SaveAsync();

        var result = _mapper.Map<GoodsReceiptTemporaryHeaderDto>(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: GoodsReceiptTemporary updated Id: {HeaderId}.", nameof(GoodsReceiptTemporaryService), nameof(UpdateGoodsReceiptTemporary), dto.GoodsReceiptTemporaryHeaderId);

        return ResponseInfo<GoodsReceiptTemporaryHeaderDto?>.Success(result, HttpStatusCode.OK, "GoodsReceiptTemporary updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteGoodsReceiptTemporary(long headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting GoodsReceiptTemporary Id: {HeaderId}", headerId);

        var entity = await _repository.GetByIdWithDetailsAsync(headerId);

        if (entity == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: GoodsReceiptTemporary not found Id: {HeaderId}.", nameof(GoodsReceiptTemporaryService), nameof(DeleteGoodsReceiptTemporary), headerId);
            return ResponseInfo<bool>.Failure("GoodsReceiptTemporary not found.", HttpStatusCode.NotFound);
        }

        await _repository.DeleteWithDetailsAsync(headerId);

        _logger.LogInformation("{ClassName} - {MethodName} Information: GoodsReceiptTemporary deleted Id: {HeaderId}.", nameof(GoodsReceiptTemporaryService), nameof(DeleteGoodsReceiptTemporary), headerId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "GoodsReceiptTemporary deleted successfully.");
    }

    public async Task<ResponseInfo<bool>> IsExist(long headerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Checking GoodsReceiptTemporary existence HeaderId: {HeaderId}", headerId);

        var result = await _repository.IsExistByIdAsync(headerId, nameof(GoodsReceiptTemporaryHeader.GoodsReceiptTemporaryHeaderId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: GoodsReceiptTemporary exists: {Result} HeaderId: {HeaderId}.", nameof(GoodsReceiptTemporaryService), nameof(IsExist), result, headerId);

        return ResponseInfo<bool>.Success(result, HttpStatusCode.OK, result ? "GoodsReceiptTemporary exists." : "GoodsReceiptTemporary does not exist.");
    }
}