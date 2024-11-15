using GeekShopping.CouponAPI.DTO;
using GeekShopping.CouponAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CouponController : Controller
    {
        private readonly ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<CouponDTO>>> GetAll()
        {
            var coupons = await _couponRepository.GetCoupons();
            return Ok(coupons);
        }

        //[Authorize]
        [HttpGet("{couponCode}/code")]
        public async Task<ActionResult<CouponDTO>> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _couponRepository.GetCouponByCouponCode(couponCode);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponDTO>> GetById(long id)
        {
            var coupon = await _couponRepository.GetCoupon(id);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<CouponDTO>> Post([FromBody] CouponDTO couponDTO)
        {
            if (couponDTO == null) return BadRequest();
            var coupon = await _couponRepository.Create(couponDTO);
            return Ok(coupon);
        }

        //[Authorize]
        [HttpPut]
        public async Task<ActionResult<CouponDTO>> Put([FromBody] CouponDTO couponDTO)
        {
            if (couponDTO == null) return BadRequest();
            var coupon = await _couponRepository.Update(couponDTO);
            return Ok(coupon);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            if(await _couponRepository.GetCoupon(id) == null) return NotFound();
            bool status = await _couponRepository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}
