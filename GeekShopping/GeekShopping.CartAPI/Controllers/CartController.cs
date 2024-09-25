using GeekShopping.CartAPI.DTO;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQSender;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartRepository _repository;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository repository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _repository = repository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetCartById(string id)
        {
            var cart = await _repository.GetCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<CartDTO>> AddCartItem(CartDTO cartDTO)
        {
            var cart = await _repository.SaveOrUpdateCart(cartDTO);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut]
        public async Task<ActionResult<CartDTO>> UpdateCartItem(CartDTO cartDTO)
        {
            var cart = await _repository.SaveOrUpdateCart(cartDTO);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CartDTO>> RemoveCartItem(int id)
        {
            var status = await _repository.RemoveCartItem(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO cartDTO)
        {
            var status = await _repository
                .ApplyCoupon(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartDTO>> RemoveCoupon(string userId)
        {
            var status = await _repository.RemoveCoupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO checkoutHeaderDTO)
        {
            if (checkoutHeaderDTO?.UserId == null) return BadRequest();

            var cart = await _repository.GetCartByUserId(checkoutHeaderDTO.UserId);

            if (cart == null) return NotFound();

            checkoutHeaderDTO.CartDetails = cart.CartDetails;
            checkoutHeaderDTO.DateTime = DateTime.Now;

            _rabbitMQMessageSender.SendMessage(checkoutHeaderDTO, "checkoutqueue");

            return Ok(checkoutHeaderDTO);
        }
    }
}
