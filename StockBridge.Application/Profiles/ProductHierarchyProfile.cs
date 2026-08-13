using AutoMapper;
using StockBridge.Application.DTOs.ProductHierarchies;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class ProductHierarchyProfile : Profile
{
    public ProductHierarchyProfile()
    {
        CreateMap<ProductHierarchy, ProductHierarchyDto>();
        CreateMap<ProductHierarchyDto, ProductHierarchy>();
    }
}