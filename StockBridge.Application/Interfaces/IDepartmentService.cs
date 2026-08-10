using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Departments;

namespace StockBridge.Application.Interfaces;

public interface IDepartmentService
{
    Task<ResponseInfo<List<DepartmentDto>?>> GetAllDepartments(CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<DepartmentDto>?>> GetAllDepartments(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    Task<ResponseInfo<DepartmentDto?>> GetDepartmentById(int id, CancellationToken cancellationToken = default);
    Task<ResponseInfo<List<DepartmentDto>?>> SearchDepartment(string fieldName, string searchString, CancellationToken cancellationToken = default);
    Task<ResponseInfo<DepartmentDto?>> AddDepartment(DepartmentDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> UpdateDepartment(DepartmentDto dto, CancellationToken cancellationToken = default);
    Task<ResponseInfo<bool>> DeleteDepartment(int id, CancellationToken cancellationToken = default);
}