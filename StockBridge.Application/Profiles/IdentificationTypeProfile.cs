using AutoMapper;
using StockBridge.Application.DTOs.IdentificationTypes;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class IdentificationTypeProfile : Profile
{
    public IdentificationTypeProfile()
    {
        CreateMap<IdentificationType, IdentificationTypeDto>();
        CreateMap<IdentificationTypeDto, IdentificationType>();
    }
}