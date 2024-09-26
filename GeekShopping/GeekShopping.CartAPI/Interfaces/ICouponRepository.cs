using GeekShopping.CartAPI.DTO;

namespace GeekShopping.CartAPI.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCoupon(string couponCode, string token);
    }
}
