using AutoMapper;
using StockControl.Application.DTOs.Products;
using StockControl.Domain.Entities;

namespace StockControl.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();
        }
    }
}