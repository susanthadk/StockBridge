using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Employees;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(
        IRepository<Employee> employeeRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<EmployeeService> logger)
    {
        _employeeRepository = employeeRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<EmployeeDto>?>> GetAllEmployees(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Employees.");

        var result = await _employeeRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Employees found.", nameof(EmployeeService), nameof(GetAllEmployees));
            return ResponseInfo<List<EmployeeDto>?>.Success(new List<EmployeeDto>(), HttpStatusCode.NoContent, "No Employees found.");
        }

        var dtos = _mapper.Map<List<EmployeeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Employees.", nameof(EmployeeService), nameof(GetAllEmployees), dtos.Count);

        return ResponseInfo<List<EmployeeDto>?>.Success(dtos, HttpStatusCode.OK, "Employees retrieved successfully.");
    }

    public async Task<ResponseInfo<List<EmployeeDto>?>> GetAllEmployees(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Employees. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _employeeRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Employees found.", nameof(EmployeeService), nameof(GetAllEmployees));
            return ResponseInfo<List<EmployeeDto>?>.Success(new List<EmployeeDto>(), HttpStatusCode.NoContent, "No Employees found.");
        }

        var dtos = _mapper.Map<List<EmployeeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Employees.", nameof(EmployeeService), nameof(GetAllEmployees), dtos.Count);

        return ResponseInfo<List<EmployeeDto>?>.Success(dtos, HttpStatusCode.OK, "Employees retrieved successfully.");
    }

    public async Task<ResponseInfo<EmployeeDto?>> GetEmployeeById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Employee Id: {EmployeeId}", id);

        var result = await _employeeRepository.GetByIdAsync(id, nameof(Employee.EmployeeId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Employee not found Id: {EmployeeId}.", nameof(EmployeeService), nameof(GetEmployeeById), id);
            return ResponseInfo<EmployeeDto?>.Success(null, HttpStatusCode.NoContent, "Employee not found.");
        }

        var dto = _mapper.Map<EmployeeDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Employee Id: {EmployeeId}.", nameof(EmployeeService), nameof(GetEmployeeById), id);

        return ResponseInfo<EmployeeDto?>.Success(dto, HttpStatusCode.OK, "Employee retrieved successfully.");
    }

    public async Task<ResponseInfo<List<EmployeeDto>?>> SearchEmployee(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching Employees by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _employeeRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Employees found.", nameof(EmployeeService), nameof(SearchEmployee));
            return ResponseInfo<List<EmployeeDto>?>.Success(new List<EmployeeDto>(), HttpStatusCode.NoContent, "No Employees found.");
        }

        var dtos = _mapper.Map<List<EmployeeDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Employees.", nameof(EmployeeService), nameof(SearchEmployee), dtos.Count);

        return ResponseInfo<List<EmployeeDto>?>.Success(dtos, HttpStatusCode.OK, "Employees retrieved successfully.");
    }

    public async Task<ResponseInfo<EmployeeDto?>> AddEmployee(EmployeeDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Employee.");

        var existing = await _employeeRepository.GetByFieldAsync("EmployeeCode", dto.EmployeeCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Employee already exists with the same Employee Code.", nameof(EmployeeService), nameof(AddEmployee));
            return ResponseInfo<EmployeeDto?>.Failure("Employee already exists with the same Employee Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Employee>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _employeeRepository.AddAsync(entity);

        var resultDto = _mapper.Map<EmployeeDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Employee added successfully EmployeeId: {EmployeeId}.", nameof(EmployeeService), nameof(AddEmployee), result.EmployeeId);

        return ResponseInfo<EmployeeDto?>.Success(resultDto, HttpStatusCode.Created, "Employee added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateEmployee(EmployeeDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Employee Id: {EmployeeId}", dto.EmployeeId);

        var isExists = await _employeeRepository.IsExistByIdAsync(dto.EmployeeId, nameof(Employee.EmployeeId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Employee not found Id: {EmployeeId}.", nameof(EmployeeService), nameof(UpdateEmployee), dto.EmployeeId);
            return ResponseInfo<bool>.Failure("Employee not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<Employee>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _employeeRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Employee updated Id: {EmployeeId}.", nameof(EmployeeService), nameof(UpdateEmployee), dto.EmployeeId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Employee updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteEmployee(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Employee Id: {EmployeeId}", id);

        var isExists = await _employeeRepository.IsExistByIdAsync(id, nameof(Employee.EmployeeId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Employee not found Id: {EmployeeId}.", nameof(EmployeeService), nameof(DeleteEmployee), id);
            return ResponseInfo<bool>.Failure("Employee not found.", HttpStatusCode.NotFound);
        }

        await _employeeRepository.DeleteAsync(id, nameof(Employee.EmployeeId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Employee deleted Id: {EmployeeId}.", nameof(EmployeeService), nameof(DeleteEmployee), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Employee deleted successfully.");
    }
}