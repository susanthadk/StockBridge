using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Customers;

namespace StockBridge.Application.Interfaces;

public interface ICustomerService
{
    Task<ResponseInfo<List<CustomerDto>?>> GetAllCustomers(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<CustomerDto>?>> GetAllCustomers(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<CustomerDto?>> GetCustomerById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<CustomerDto>?>> SearchCustomer(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<CustomerDto?>> AddCustomer(CustomerDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateCustomer(CustomerDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteCustomer(int id, CancellationToken cancellationToken = default);
}