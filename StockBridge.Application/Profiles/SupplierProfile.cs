using AutoMapper;
using StockBridge.Application.DTOs.Suppliers;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<Supplier, SupplierDto>();
        CreateMap<SupplierDto, Supplier>();
    }
}