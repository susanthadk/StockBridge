using AutoMapper;
using StockBridge.Application.DTOs.OrganizationalLevels;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class OrganizationalLevelProfile : Profile
{
    public OrganizationalLevelProfile()
    {
        CreateMap<OrganizationalLevel, OrganizationalLevelDto>();
        CreateMap<OrganizationalLevelDto, OrganizationalLevel>();
    }
}