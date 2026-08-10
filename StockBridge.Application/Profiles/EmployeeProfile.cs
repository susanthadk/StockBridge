using AutoMapper;
using StockBridge.Application.DTOs.Employees;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();
    }
}