using AutoMapper;
using StockBridge.Application.DTOs.OrganizationalUnits;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class OrganizationalUnitProfile : Profile
{
    public OrganizationalUnitProfile()
    {
        CreateMap<OrganizationalUnit, OrganizationalUnitDto>();
        CreateMap<OrganizationalUnitDto, OrganizationalUnit>();
    }
}