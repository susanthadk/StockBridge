using AutoMapper;
using StockBridge.Application.DTOs.Brands;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandDto>();
        CreateMap<BrandDto, Brand>();
    }
}