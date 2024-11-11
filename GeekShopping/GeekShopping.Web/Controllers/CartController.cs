using GeekShopping.Web.DTO;
using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IOrderRepository _orderRepository;

        public CartController(
            ICartRepository cartRepository,
            ICouponRepository couponService,
            IOrderRepository orderRepository
        )
        {
            _couponRepository = couponService;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
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

            OrderHeader order = new()
            {
                UserId = cart.CartHeader.UserId,
                FirstName = cartDTO.CartHeader.FirstName,
                LastName = cartDTO.CartHeader.LastName,
                OrderDetails = new List<OrderDetail>(),
                CardNumber = cartDTO.CartHeader.CardNumber,
                CouponCode = cart.CartHeader.CouponCode,
                CVV = cartDTO.CartHeader.CVV,
                DiscountAmount = cartDTO.CartHeader.DiscountAmount,
                Email = cartDTO.CartHeader.Email,
                ExpireDate = cartDTO.CartHeader.ExpireDate,
                OrderTime = DateTime.Now,
                PurchaseAmount = cartDTO.CartHeader.PurchaseAmount,
                PaymentStatus = false,
                Phone = cartDTO.CartHeader.Phone,
                DateTime = DateTime.Now
            };

            foreach (var cartDetail in cart.CartDetails)
            {
                OrderDetail detail = new()
                {
                    ProductId = cartDetail.ProductId,
                    ProductName = cartDetail.Product.Name,
                    Price = cartDetail.Product.Price,
                    Count = cartDetail.Count,
                };
                order.CartTotalItems += cartDetail.Count;
                order.OrderDetails.Add(detail);
            }

            var orderDb = await _orderRepository.AddOrder(order);
            
            await _cartRepository.ClearCart(userId);

            await _orderRepository.UpdateOrderPaymentStatus(orderDb.Id, true);

            return RedirectToAction(nameof(Confirmation));
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
