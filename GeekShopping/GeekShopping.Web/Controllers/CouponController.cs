using GeekShopping.Web.Models;
using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace GeekShopping.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> Index()
        {
            var accessToken = await GetAccessToken();
            var coupons = await _couponService.GetCoupons(accessToken);
            return View(coupons);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await GetAccessToken();
                var response = await _couponService.Create(coupon, accessToken);
                if (response != null) return RedirectToAction(nameof(Index));
            }
            return View(coupon);
        }
        public async Task<IActionResult> Update(int id)
        {
            var accessToken = await GetAccessToken();
            var coupon = await _couponService.GetCoupon(id, accessToken);
            return View(coupon);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await GetAccessToken();
                var response = await _couponService.Update(coupon, accessToken);
                if (response != null) return RedirectToAction(nameof(Index));
            }
            return View(coupon);
        }
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var accessToken = await GetAccessToken();
            var response = await _couponService.Delete(id, accessToken);
            if (response) return RedirectToAction(nameof(Index));
            return View();
        }

        private Task<string> GetAccessToken()
        {
            return HttpContext.GetTokenAsync("access_token");
        }
    }
}