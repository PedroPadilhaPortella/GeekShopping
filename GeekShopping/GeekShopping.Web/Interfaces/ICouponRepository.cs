using GeekShopping.Web.DTO;

namespace GeekShopping.Web.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCoupon(string couponCode);
        Task<CouponDTO> GetCoupon(long id);
        Task<List<CouponDTO>> GetCoupons();
        Task<CouponDTO> Create(CouponDTO couponDTO);
        Task<CouponDTO> Update(CouponDTO couponDTO);
        Task<bool> Delete(long id);
    }
}
