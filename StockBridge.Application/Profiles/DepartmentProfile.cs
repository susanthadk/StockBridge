using AutoMapper;
using StockBridge.Application.DTOs.Departments;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentDto>();
        CreateMap<DepartmentDto, Department>();
    }
}