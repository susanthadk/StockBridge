using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Employees;

namespace StockBridge.Application.Interfaces;

public interface IEmployeeService
{
    Task<ResponseInfo<List<EmployeeDto>?>> GetAllEmployees(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<EmployeeDto>?>> GetAllEmployees(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<EmployeeDto?>> GetEmployeeById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<EmployeeDto>?>> SearchEmployee(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<EmployeeDto?>> AddEmployee(EmployeeDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateEmployee(EmployeeDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteEmployee(int id, CancellationToken cancellationToken = default);
}