using GeekShopping.Web.DTO;

namespace GeekShopping.Web.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCoupon(string couponCode);
    }
}
