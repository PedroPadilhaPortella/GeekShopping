using GeekShopping.Web.DTO;
using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICartRepository _cartRepository;
        private readonly ICouponRepository _couponRepository;

        public CartController(
            ICartService cartService,
            ICartRepository cartRepository,
            ICouponRepository couponService
        )
        {
            _cartService = cartService;
            _couponRepository = couponService;
            _cartRepository = cartRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var cart = await _cartRepository.GetCartByUserId(userId);

            if (cart.CartHeader != null)
            {

                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    var coupon = await _couponRepository.GetCoupon(cart.CartHeader.CouponCode);

                    if (coupon?.Code != null)
                    {
                        cart.CartHeader.DiscountAmount = coupon.DiscountAmount;
                    }
                }
                foreach (var detail in cart.CartDetails)
                {
                    cart.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
                }

                cart.CartHeader.PurchaseAmount -= cart.CartHeader.DiscountAmount;
            }

            return View(cart);
        }

        [Authorize]
        public async Task<IActionResult> Remove(long id)
        {
            var response = await _cartRepository.RemoveCartItem(id);

            if (response) return RedirectToAction(nameof(Index));

            return View();
        }

        [Authorize]
        [HttpPost("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDTO cart)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var response = await _cartRepository.ApplyCoupon(userId, cart.CartHeader.CouponCode);

            if (response) return RedirectToAction(nameof(Index));
            return View();
        }

        [Authorize]
        [HttpPost("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartRepository.RemoveCoupon(userId);

            if (response) return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var cart = await _cartRepository.GetCartByUserId(userId);

            if (cart.CartHeader != null)
            {

                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    var coupon = await _couponRepository.GetCoupon(cart.CartHeader.CouponCode);

                    if (coupon?.Code != null)
                    {
                        cart.CartHeader.DiscountAmount = coupon.DiscountAmount;
                    }
                }
                foreach (var detail in cart.CartDetails)
                {
                    cart.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
                }

                cart.CartHeader.PurchaseAmount -= cart.CartHeader.DiscountAmount;
            }

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartDTO cartDTO)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var cart = await _cartRepository.GetCartByUserId(userId);


            //var response = await _cartService.Checkout(cart.CartHeader, accessToken);

            //if (response != null && response.GetType() == typeof(string)) {
            //    TempData["Error"] = response;
            //    return RedirectToAction(nameof(Checkout));
            //}
            //else if (response != null) {
            //    return RedirectToAction(nameof(Confirmation));
            //}

            return View(cart);
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
