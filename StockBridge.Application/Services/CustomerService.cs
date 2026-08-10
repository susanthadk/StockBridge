using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Customers;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(
        IRepository<Customer> customerRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<CustomerService> logger)
    {
        _customerRepository = customerRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<CustomerDto>?>> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Customers.");

        var result = await _customerRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Customers found.", nameof(CustomerService), nameof(GetAllCustomers));
            return ResponseInfo<List<CustomerDto>?>.Success(new List<CustomerDto>(), HttpStatusCode.NoContent, "No Customers found.");
        }

        var dtos = _mapper.Map<List<CustomerDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Customers.", nameof(CustomerService), nameof(GetAllCustomers), dtos.Count);

        return ResponseInfo<List<CustomerDto>?>.Success(dtos, HttpStatusCode.OK, "Customers retrieved successfully.");
    }

    public async Task<ResponseInfo<List<CustomerDto>?>> GetAllCustomers(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Customers. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _customerRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Customers found.", nameof(CustomerService), nameof(GetAllCustomers));
            return ResponseInfo<List<CustomerDto>?>.Success(new List<CustomerDto>(), HttpStatusCode.NoContent, "No Customers found.");
        }

        var dtos = _mapper.Map<List<CustomerDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Customers.", nameof(CustomerService), nameof(GetAllCustomers), dtos.Count);

        return ResponseInfo<List<CustomerDto>?>.Success(dtos, HttpStatusCode.OK, "Customers retrieved successfully.");
    }

    public async Task<ResponseInfo<CustomerDto?>> GetCustomerById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Customer Id: {CustomerId}", id);

        var result = await _customerRepository.GetByIdAsync(id, nameof(Customer.CustomerId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Customer not found Id: {CustomerId}.", nameof(CustomerService), nameof(GetCustomerById), id);
            return ResponseInfo<CustomerDto?>.Success(null, HttpStatusCode.NoContent, "Customer not found.");
        }

        var dto = _mapper.Map<CustomerDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Customer Id: {CustomerId}.", nameof(CustomerService), nameof(GetCustomerById), id);

        return ResponseInfo<CustomerDto?>.Success(dto, HttpStatusCode.OK, "Customer retrieved successfully.");
    }

    public async Task<ResponseInfo<List<CustomerDto>?>> SearchCustomer(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching Customers by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _customerRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Customers found.", nameof(CustomerService), nameof(SearchCustomer));
            return ResponseInfo<List<CustomerDto>?>.Success(new List<CustomerDto>(), HttpStatusCode.NoContent, "No Customers found.");
        }

        var dtos = _mapper.Map<List<CustomerDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Customers.", nameof(CustomerService), nameof(SearchCustomer), dtos.Count);

        return ResponseInfo<List<CustomerDto>?>.Success(dtos, HttpStatusCode.OK, "Customers retrieved successfully.");
    }

    public async Task<ResponseInfo<CustomerDto?>> AddCustomer(CustomerDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Customer.");

        var existing = await _customerRepository.GetByFieldAsync("CustomerCode", dto.CustomerCode);
        if (existing?.Any() == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Customer already exists with the same Customer Code.", nameof(CustomerService), nameof(AddCustomer));
            return ResponseInfo<CustomerDto?>.Failure("Customer already exists with the same Customer Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Customer>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _customerRepository.AddAsync(entity);

        var resultDto = _mapper.Map<CustomerDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Customer added successfully CustomerId: {CustomerId}.", nameof(CustomerService), nameof(AddCustomer), result.CustomerId);

        return ResponseInfo<CustomerDto?>.Success(resultDto, HttpStatusCode.Created, "Customer added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateCustomer(CustomerDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Customer Id: {CustomerId}", dto.CustomerId);

        var isExists = await _customerRepository.IsExistByIdAsync(dto.CustomerId, nameof(Customer.CustomerId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Customer not found Id: {CustomerId}.", nameof(CustomerService), nameof(UpdateCustomer), dto.CustomerId);
            return ResponseInfo<bool>.Failure("Customer not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<Customer>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _customerRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Customer updated Id: {CustomerId}.", nameof(CustomerService), nameof(UpdateCustomer), dto.CustomerId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Customer updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteCustomer(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Customer Id: {CustomerId}", id);

        var isExists = await _customerRepository.IsExistByIdAsync(id, nameof(Customer.CustomerId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Customer not found Id: {CustomerId}.", nameof(CustomerService), nameof(DeleteCustomer), id);
            return ResponseInfo<bool>.Failure("Customer not found.", HttpStatusCode.NotFound);
        }

        await _customerRepository.DeleteAsync(id, nameof(Customer.CustomerId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Customer deleted Id: {CustomerId}.", nameof(CustomerService), nameof(DeleteCustomer), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Customer deleted successfully.");
    }
}