using GeekShopping.Web.DTO;
using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponRepository _couponRepository;


        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<IActionResult> Index()
        {
            var coupons = await _couponRepository.GetCoupons();
            return View(coupons);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CouponDTO coupon)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponRepository.Create(coupon);
                if (response != null) return RedirectToAction(nameof(Index));
            }
            return View(coupon);
        }

        public async Task<IActionResult> Update(int id)
        {
            var coupon = await _couponRepository.GetCoupon(id);
            return View(coupon);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(CouponDTO coupon)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponRepository.Update(coupon);
                if (response != null) return RedirectToAction(nameof(Index));
            }
            return View(coupon);
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _couponRepository.Delete(id);
            if (response) return RedirectToAction(nameof(Index));
            return View();
        }
    }
}
