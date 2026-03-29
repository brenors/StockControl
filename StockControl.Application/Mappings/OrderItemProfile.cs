using AutoMapper;
using StockControl.Application.DTOs.Orders;
using StockControl.Domain.Entities;

namespace StockControl.Application.Mappings
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.UnitPrice * src.Quantity));
        }
    }
}
