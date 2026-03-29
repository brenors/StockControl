using AutoMapper;
using StockControl.Application.DTOs.Stocks;
using StockControl.Domain.Entities;

namespace StockControl.Application.Mappings
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<Stock, StockResponse>();
        }
    }
}
