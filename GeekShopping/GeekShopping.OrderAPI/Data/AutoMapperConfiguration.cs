using AutoMapper;
using GeekShopping.OrderAPI.DTO;
using GeekShopping.OrderAPI.Models;

namespace GeekShopping.OrderAPI.Data
{
  public class AutoMapperConfiguration
  {
    public static MapperConfiguration RegisterMaps()
    {
      return new MapperConfiguration((config) =>
      {
        config.CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
        config.CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
      });
    }
  }
}