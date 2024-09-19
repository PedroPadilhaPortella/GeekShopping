using GeekShopping.CouponAPI.DTO;

namespace GeekShopping.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponBtCouponCode(string couponCode);
    }
}
