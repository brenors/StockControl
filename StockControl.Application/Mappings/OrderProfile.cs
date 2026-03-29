using AutoMapper;
using StockControl.Application.DTOs.Orders;
using StockControl.Domain.Entities;

namespace StockControl.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>();
        }
    }
}
