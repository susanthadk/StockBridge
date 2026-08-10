using AutoMapper;
using StockBridge.Application.DTOs.Customers;
using StockBridge.Domain.Entities;

namespace StockBridge.Application.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerDto, Customer>();
    }
}