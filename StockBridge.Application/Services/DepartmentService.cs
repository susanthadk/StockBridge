using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Departments;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IRepository<Department> _departmentRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(
        IRepository<Department> departmentRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<DepartmentService> logger)
    {
        _departmentRepository = departmentRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<DepartmentDto>?>> GetAllDepartments(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Departments.");

        var result = await _departmentRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Departments found.", nameof(DepartmentService), nameof(GetAllDepartments));
            return ResponseInfo<List<DepartmentDto>?>.Success(new List<DepartmentDto>(), HttpStatusCode.NoContent, "No Departments found.");
        }

        var dtos = _mapper.Map<List<DepartmentDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Departments.", nameof(DepartmentService), nameof(GetAllDepartments), dtos.Count);

        return ResponseInfo<List<DepartmentDto>?>.Success(dtos, HttpStatusCode.OK, "Departments retrieved successfully.");
    }

    public async Task<ResponseInfo<List<DepartmentDto>?>> GetAllDepartments(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Departments. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _departmentRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Departments found.", nameof(DepartmentService), nameof(GetAllDepartments));
            return ResponseInfo<List<DepartmentDto>?>.Success(new List<DepartmentDto>(), HttpStatusCode.NoContent, "No Departments found.");
        }

        var dtos = _mapper.Map<List<DepartmentDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Departments.", nameof(DepartmentService), nameof(GetAllDepartments), dtos.Count);

        return ResponseInfo<List<DepartmentDto>?>.Success(dtos, HttpStatusCode.OK, "Departments retrieved successfully.");
    }

    public async Task<ResponseInfo<DepartmentDto?>> GetDepartmentById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Department Id: {DepartmentId}", id);

        var result = await _departmentRepository.GetByIdAsync(id, nameof(Department.DepartmentId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Department not found Id: {DepartmentId}.", nameof(DepartmentService), nameof(GetDepartmentById), id);
            return ResponseInfo<DepartmentDto?>.Success(null, HttpStatusCode.NoContent, "Department not found.");
        }

        var dto = _mapper.Map<DepartmentDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Department Id: {DepartmentId}.", nameof(DepartmentService), nameof(GetDepartmentById), id);

        return ResponseInfo<DepartmentDto?>.Success(dto, HttpStatusCode.OK, "Department retrieved successfully.");
    }

    public async Task<ResponseInfo<List<DepartmentDto>?>> SearchDepartment(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching Departments by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _departmentRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Departments found.", nameof(DepartmentService), nameof(SearchDepartment));
            return ResponseInfo<List<DepartmentDto>?>.Success(new List<DepartmentDto>(), HttpStatusCode.NoContent, "No Departments found.");
        }

        var dtos = _mapper.Map<List<DepartmentDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Departments.", nameof(DepartmentService), nameof(SearchDepartment), dtos.Count);

        return ResponseInfo<List<DepartmentDto>?>.Success(dtos, HttpStatusCode.OK, "Departments retrieved successfully.");
    }

    public async Task<ResponseInfo<DepartmentDto?>> AddDepartment(DepartmentDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Department.");

        var existing = await _departmentRepository.GetByFieldAsync("DepartmentCode", dto.DepartmentCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Department already exists with the same Department Code.", nameof(DepartmentService), nameof(AddDepartment));
            return ResponseInfo<DepartmentDto?>.Failure("Department already exists with the same Department Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Department>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _departmentRepository.AddAsync(entity);

        var resultDto = _mapper.Map<DepartmentDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Department added successfully DepartmentId: {DepartmentId}.", nameof(DepartmentService), nameof(AddDepartment), result.DepartmentId);

        return ResponseInfo<DepartmentDto?>.Success(resultDto, HttpStatusCode.Created, "Department added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateDepartment(DepartmentDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Department Id: {DepartmentId}", dto.DepartmentId);

        var isExists = await _departmentRepository.IsExistByIdAsync(dto.DepartmentId, nameof(Department.DepartmentId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Department not found Id: {DepartmentId}.", nameof(DepartmentService), nameof(UpdateDepartment), dto.DepartmentId);
            return ResponseInfo<bool>.Failure("Department not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<Department>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _departmentRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Department updated Id: {DepartmentId}.", nameof(DepartmentService), nameof(UpdateDepartment), dto.DepartmentId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Department updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteDepartment(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Department Id: {DepartmentId}", id);

        var isExists = await _departmentRepository.IsExistByIdAsync(id, nameof(Department.DepartmentId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Department not found Id: {DepartmentId}.", nameof(DepartmentService), nameof(DeleteDepartment), id);
            return ResponseInfo<bool>.Failure("Department not found.", HttpStatusCode.NotFound);
        }

        await _departmentRepository.DeleteAsync(id, nameof(Department.DepartmentId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Department deleted Id: {DepartmentId}.", nameof(DepartmentService), nameof(DeleteDepartment), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Department deleted successfully.");
    }
}