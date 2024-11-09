using AutoMapper;
using GeekShopping.Web.DTO;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Data
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