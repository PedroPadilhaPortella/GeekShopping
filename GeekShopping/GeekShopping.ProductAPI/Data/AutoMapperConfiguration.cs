using AutoMapper;
using GeekShopping.ProductAPI.DTO;
using GeekShopping.ProductAPI.Models;

namespace GeekShopping.ProductAPI.Data
{
  public class AutoMapperConfiguration
  {
    public static MapperConfiguration RegisterMaps()
    {
      return new MapperConfiguration((config) =>
      {
        config.CreateMap<Product, ProductDTO>().ReverseMap();
      });
    }
  }
}