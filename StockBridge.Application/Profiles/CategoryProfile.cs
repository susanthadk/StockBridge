using AutoMapper;
using StockBridge.Application.DTOs.Categories;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
    }
}