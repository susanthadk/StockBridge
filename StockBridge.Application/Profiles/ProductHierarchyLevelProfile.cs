using AutoMapper;
using StockBridge.Application.DTOs.ProductHierarchyLevels;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class ProductHierarchyLevelProfile : Profile
{
    public ProductHierarchyLevelProfile()
    {
        CreateMap<ProductHierarchyLevel, ProductHierarchyLevelDto>();
        CreateMap<ProductHierarchyLevelDto, ProductHierarchyLevel>();
    }
}