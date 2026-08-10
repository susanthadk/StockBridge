using AutoMapper;
using StockBridge.Application.DTOs.AccountInformations;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class AccountInformationProfile : Profile
{
    public AccountInformationProfile()
    {
        CreateMap<AccountInformation, AccountInformationDto>();
        CreateMap<AccountInformationDto, AccountInformation>();
    }
}