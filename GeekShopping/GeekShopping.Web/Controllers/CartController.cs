using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(
            IProductService productService,
            ICartService cartService,
            ICouponService couponService
        )
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cart = await RetrieveCart();
            return View(cart);
        }

        [Authorize]
        public async Task<IActionResult> Remove(long id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveFromCart(id, accessToken);

            if (response) return RedirectToAction(nameof(Index));

            return View();
        }

        [Authorize]
        [HttpPost("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(Cart cart)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.ApplyCoupon(cart, accessToken);

            if (response) return RedirectToAction(nameof(Index));
            return View();
        }

        [Authorize]
        [HttpPost("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.RemoveCoupon(userId, accessToken);

            if (response) return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var cart = await RetrieveCart();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Cart cart)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.Checkout(cart.CartHeader, accessToken);

            if (response != null && response.GetType() == typeof(string)) {
                TempData["Error"] = response;
                return RedirectToAction(nameof(Checkout));
            }
            else if (response != null) {
                return RedirectToAction(nameof(Confirmation));
            }
            
            return View(cart);
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }



        private async Task<Cart> RetrieveCart()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.FindCardByUserId(userId, accessToken);

            if (response?.CartHeader != null) {

                if (!string.IsNullOrEmpty(response.CartHeader.CouponCode)) {
                    var coupon = await _couponService.GetCoupon(response.CartHeader.CouponCode, accessToken);

                    if (coupon?.Code != null) {
                        response.CartHeader.DiscountAmount = coupon.DiscountAmount;
                    }
                }
                foreach (var detail in response.CartDetails)
                {
                    response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
                }

                response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountAmount;
            }
            return response;
        }
    }
}
