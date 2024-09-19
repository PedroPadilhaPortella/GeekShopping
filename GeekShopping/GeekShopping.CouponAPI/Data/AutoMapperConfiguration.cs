using AutoMapper;
using GeekShopping.CouponAPI.DTO;
using GeekShopping.CouponAPI.Models;

namespace GeekShopping.CouponAPI.Data
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration((config) =>
            {
                config.CreateMap<Coupon, CouponDTO>().ReverseMap();
            });
        }
    }
}