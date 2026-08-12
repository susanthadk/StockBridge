using AutoMapper;
using StockBridge.Application.DTOs.SupplierTypes;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class SupplierTypeProfile : Profile
{
    public SupplierTypeProfile()
    {
        CreateMap<SupplierType, SupplierTypeDto>();
        CreateMap<SupplierTypeDto, SupplierType>();
    }
}
