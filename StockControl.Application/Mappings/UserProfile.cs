using AutoMapper;
using StockControl.Application.DTOs.Auth;
using StockControl.Domain.Entities;

namespace StockControl.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, AuthResponse>();
            CreateMap<RegisterRequest, User>();
            CreateMap<LoginRequest, User>();
        }
    }
}
