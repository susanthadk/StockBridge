using AutoMapper;
using StockBridge.Application.DTOs.Accounts;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDto>();
        CreateMap<AccountDto, Account>();
    }
}