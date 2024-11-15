using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
    public interface ICouponService
    {
        Task<Coupon> GetCoupon(string code, string token);
        Task<Coupon> GetCoupon(long id, string token);
        Task<IEnumerable<Coupon>> GetCoupons(string token);
        Task<Coupon> Create(Coupon coupon, string token);
        Task<Coupon> Update(Coupon coupon, string token);
        Task<bool> Delete(long id, string token);
    }
}
