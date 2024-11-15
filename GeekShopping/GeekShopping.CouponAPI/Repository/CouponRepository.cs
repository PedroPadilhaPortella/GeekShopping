using AutoMapper;
using GeekShopping.CouponAPI.Data;
using GeekShopping.CouponAPI.DTO;
using GeekShopping.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Repository
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

        public async Task<CouponDTO> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == couponCode);
            return _mapper.Map<CouponDTO>(coupon);
        }

        public async Task<CouponDTO> GetCoupon(long id)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<CouponDTO>(coupon);
        }
        public async Task<List<CouponDTO>> GetCoupons()
        {
            List<Coupon> coupons = await _context.Coupons.ToListAsync();
            return _mapper.Map<List<CouponDTO>>(coupons);
        }
        public async Task<CouponDTO> Create(CouponDTO couponDTO)
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDTO);
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            return _mapper.Map<CouponDTO>(coupon);
        }
        public async Task<CouponDTO> Update(CouponDTO couponDTO)
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDTO);
            _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();
            return _mapper.Map<CouponDTO>(coupon);
        }
        public async Task<bool> Delete(long id)
        {
            try
            {
                Coupon coupon = await _context.Coupons
                  .Where(c => c.Id == id).FirstOrDefaultAsync()
                  ?? new Coupon(); ;
                if (coupon.Id <= 0) return false;
                _context.Coupons.Remove(coupon);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
