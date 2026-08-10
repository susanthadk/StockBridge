using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Suppliers;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<SupplierService> _logger;

    public SupplierService(
        IRepository<Supplier> supplierRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<SupplierService> logger)
    {
        _supplierRepository = supplierRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<SupplierDto>?>> GetAllSuppliers(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Suppliers.");

        var result = await _supplierRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Suppliers found.", nameof(SupplierService), nameof(GetAllSuppliers));
            return ResponseInfo<List<SupplierDto>?>.Success(new List<SupplierDto>(), HttpStatusCode.NoContent, "No Suppliers found.");
        }

        var dtos = _mapper.Map<List<SupplierDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Suppliers.", nameof(SupplierService), nameof(GetAllSuppliers), dtos.Count);

        return ResponseInfo<List<SupplierDto>?>.Success(dtos, HttpStatusCode.OK, "Suppliers retrieved successfully.");
    }

    public async Task<ResponseInfo<List<SupplierDto>?>> GetAllSuppliers(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Suppliers. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _supplierRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Suppliers found.", nameof(SupplierService), nameof(GetAllSuppliers));
            return ResponseInfo<List<SupplierDto>?>.Success(new List<SupplierDto>(), HttpStatusCode.NoContent, "No Suppliers found.");
        }

        var dtos = _mapper.Map<List<SupplierDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Suppliers.", nameof(SupplierService), nameof(GetAllSuppliers), dtos.Count);

        return ResponseInfo<List<SupplierDto>?>.Success(dtos, HttpStatusCode.OK, "Suppliers retrieved successfully.");
    }

    public async Task<ResponseInfo<SupplierDto?>> GetSupplierById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Supplier Id: {SupplierId}", id);

        var result = await _supplierRepository.GetByIdAsync(id, nameof(Supplier.SupplierId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Supplier not found Id: {SupplierId}.", nameof(SupplierService), nameof(GetSupplierById), id);
            return ResponseInfo<SupplierDto?>.Success(null, HttpStatusCode.NoContent, "Supplier not found.");
        }

        var dto = _mapper.Map<SupplierDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Supplier Id: {SupplierId}.", nameof(SupplierService), nameof(GetSupplierById), id);

        return ResponseInfo<SupplierDto?>.Success(dto, HttpStatusCode.OK, "Supplier retrieved successfully.");
    }

    public async Task<ResponseInfo<List<SupplierDto>?>> SearchSupplier(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching Suppliers by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _supplierRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Suppliers found.", nameof(SupplierService), nameof(SearchSupplier));
            return ResponseInfo<List<SupplierDto>?>.Success(new List<SupplierDto>(), HttpStatusCode.NoContent, "No Suppliers found.");
        }

        var dtos = _mapper.Map<List<SupplierDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Suppliers.", nameof(SupplierService), nameof(SearchSupplier), dtos.Count);

        return ResponseInfo<List<SupplierDto>?>.Success(dtos, HttpStatusCode.OK, "Suppliers retrieved successfully.");
    }

    public async Task<ResponseInfo<SupplierDto?>> AddSupplier(SupplierDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Supplier.");

        var existing = await _supplierRepository.GetByFieldAsync("SupplierCode", dto.SupplierCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Supplier already exists with the same Supplier Code.", nameof(SupplierService), nameof(AddSupplier));
            return ResponseInfo<SupplierDto?>.Failure("Supplier already exists with the same Supplier Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Supplier>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _supplierRepository.AddAsync(entity);

        var resultDto = _mapper.Map<SupplierDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Supplier added successfully SupplierId: {SupplierId}.", nameof(SupplierService), nameof(AddSupplier), result.SupplierId);

        return ResponseInfo<SupplierDto?>.Success(resultDto, HttpStatusCode.Created, "Supplier added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateSupplier(SupplierDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Supplier Id: {SupplierId}", dto.SupplierId);

        var isExists = await _supplierRepository.IsExistByIdAsync(dto.SupplierId, nameof(Supplier.SupplierId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Supplier not found Id: {SupplierId}.", nameof(SupplierService), nameof(UpdateSupplier), dto.SupplierId);
            return ResponseInfo<bool>.Failure("Supplier not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<Supplier>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _supplierRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Supplier updated Id: {SupplierId}.", nameof(SupplierService), nameof(UpdateSupplier), dto.SupplierId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Supplier updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteSupplier(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Supplier Id: {SupplierId}", id);

        var isExists = await _supplierRepository.IsExistByIdAsync(id, nameof(Supplier.SupplierId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Supplier not found Id: {SupplierId}.", nameof(SupplierService), nameof(DeleteSupplier), id);
            return ResponseInfo<bool>.Failure("Supplier not found.", HttpStatusCode.NotFound);
        }

        await _supplierRepository.DeleteAsync(id, nameof(Supplier.SupplierId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Supplier deleted Id: {SupplierId}.", nameof(SupplierService), nameof(DeleteSupplier), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Supplier deleted successfully.");
    }
}