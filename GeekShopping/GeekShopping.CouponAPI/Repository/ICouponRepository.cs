using GeekShopping.CouponAPI.DTO;

namespace GeekShopping.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCouponCode(string couponCode);
        Task<CouponDTO> GetCoupon(long id);
        Task<List<CouponDTO>> GetCoupons();
        Task<CouponDTO> Create(CouponDTO couponDTO);
        Task<CouponDTO> Update(CouponDTO couponDTO);
        Task<bool> Delete(long id);
    }
}
