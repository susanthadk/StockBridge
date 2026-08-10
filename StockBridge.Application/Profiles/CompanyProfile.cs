using AutoMapper;
using StockBridge.Application.DTOs.Companies;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyDto>();
        CreateMap<CompanyDto, Company>();
    }
}