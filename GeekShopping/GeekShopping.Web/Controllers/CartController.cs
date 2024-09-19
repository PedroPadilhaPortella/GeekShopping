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

        public CartController(
            IProductService productService,
            ICartService cartService
        )
        {
            _productService = productService;
            _cartService = cartService;
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

            if(response) return RedirectToAction(nameof(Index));

            return View();
        }

        private async Task<Cart> RetrieveCart()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var response = await _cartService.FindCardByUserId(userId, accessToken);

            if (response?.CartHeader != null) {
                foreach (var detail in response.CartDetails) {
                    response.CartHeader.PurchaseAmount += detail.Product.Price * detail.Count;
                }
            }

            return response;
        }
    }
}
