using AutoMapper;
using GeekShopping.CartAPI.DTO;
using GeekShopping.CartAPI.Models;

namespace GeekShopping.CartAPI.Data
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration((config) =>
            {
                config.CreateMap<Product, ProductDTO>().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDTO>().ReverseMap();
                config.CreateMap<CartDetail, CartDetailDTO>().ReverseMap();
                config.CreateMap<Cart, CartDTO>().ReverseMap();
            });
        }
    }
}