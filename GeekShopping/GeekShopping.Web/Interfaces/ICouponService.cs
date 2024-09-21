using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
    public interface ICouponService
    {
        Task<Coupon> GetCoupon(string code, string token);
    }
}
