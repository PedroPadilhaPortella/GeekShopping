using AutoMapper;
using GeekShopping.Web.Data;
using GeekShopping.Web.DTO;
using GeekShopping.Web.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Web.Repository
{
    public class CouponRepository: ICouponRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CouponRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponDTO> GetCoupon(string couponCode)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == couponCode);
            return _mapper.Map<CouponDTO>(coupon);
        }
    }
}
