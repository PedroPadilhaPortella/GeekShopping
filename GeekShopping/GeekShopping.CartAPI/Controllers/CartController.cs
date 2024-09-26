using GeekShopping.CartAPI.DTO;
using GeekShopping.CartAPI.Interfaces;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQSender;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(
            ICartRepository cartRepository,
            IRabbitMQMessageSender rabbitMQMessageSender,
            ICouponRepository couponRepository
        ) {
            _cartRepository = cartRepository;
            _couponRepository = couponRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetCartById(string id)
        {
            var cart = await _cartRepository.GetCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<CartDTO>> AddCartItem(CartDTO cartDTO)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartDTO);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut]
        public async Task<ActionResult<CartDTO>> UpdateCartItem(CartDTO cartDTO)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartDTO);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CartDTO>> RemoveCartItem(int id)
        {
            var status = await _cartRepository.RemoveCartItem(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO cartDTO)
        {
            var status = await _cartRepository
                .ApplyCoupon(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartDTO>> RemoveCoupon(string userId)
        {
            var status = await _cartRepository.RemoveCoupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO checkoutHeaderDTO)
        {
            if (checkoutHeaderDTO?.UserId == null) return BadRequest();

            var cart = await _cartRepository.GetCartByUserId(checkoutHeaderDTO.UserId);

            if (cart == null) return NotFound();

            if(!string.IsNullOrEmpty(checkoutHeaderDTO.CouponCode))
            {
                string accessToken = Request.Headers["Authorization"];
                CouponDTO coupon = await _couponRepository.GetCoupon(checkoutHeaderDTO.CouponCode, accessToken);

                if(checkoutHeaderDTO.DiscountAmount != coupon.DiscountAmount) return StatusCode(412);
            }

            checkoutHeaderDTO.CartDetails = cart.CartDetails;
            checkoutHeaderDTO.DateTime = DateTime.Now;

            _rabbitMQMessageSender.SendMessage(checkoutHeaderDTO, "checkoutqueue");

            return Ok(checkoutHeaderDTO);
        }
    }
}
