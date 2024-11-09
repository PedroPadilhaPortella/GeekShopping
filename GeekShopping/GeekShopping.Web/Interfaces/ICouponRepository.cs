using GeekShopping.Web.DTO;

namespace GeekShopping.Web.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCoupon(string couponCode);
    }
}
