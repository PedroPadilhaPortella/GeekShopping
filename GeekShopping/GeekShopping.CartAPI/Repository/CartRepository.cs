using AutoMapper;
using GeekShopping.CartAPI.Data;
using GeekShopping.CartAPI.DTO;
using GeekShopping.CartAPI.Interfaces;
using GeekShopping.CartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
    public class CartRepository: ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartDTO> GetCartByUserId(string userId)
        {
            CartHeader cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

            if (cartHeader == null) return new CartDTO();

            Cart cart = new Cart {
                CartHeader = cartHeader,
                CartDetails = _context.CartDetails
                    .Where(c => c.CartHeaderId == cartHeader.Id)
                    .Include(c => c.Product) ?? null,
            };

            return _mapper.Map<CartDTO>(cart);
        }

        public async Task<CartDTO> SaveOrUpdateCart(CartDTO cartDTO)
        {
            Cart cart = _mapper.Map<Cart>(cartDTO);

            // Checks if the product is already saved in the database if it does not exist then save
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == cart.CartDetails.FirstOrDefault().ProductId);

            if(product == null) {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            // Check if CartHeader is null
            var cartHeader = await _context.CartHeaders
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);

            if (cartHeader == null) {
                // Create CartHeader and CartDetails
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();

            } else {
                // Check if CartDetails has same product
                var cartDetail = await _context.CartDetails
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId
                        && p.CartHeaderId == cartHeader.Id);

                if(cartDetail == null) {
                    // Create CartDetails
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                } else {
                    // Update product count and CartDetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDTO>(cart);
        }

        public async Task<bool> RemoveCartItem(long cartDetailId)
        {
            try
            {
                // Remove CartDetail
                CartDetail cartDetail = await _context.CartDetails
                    .FirstOrDefaultAsync(c => c.Id == cartDetailId);

                int total = _context.CartDetails
                    .Where(c => c.CartHeaderId == cartDetail.CartHeaderId)
                    .Count();

                _context.CartDetails.Remove(cartDetail);

                // Also remove CartHeader if total is going to be less than 1
                if (total == 1) {
                    var cartHeaderToRemove = await _context.CartHeaders
                        .FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);

                    _context.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeader = _context.CartHeaders.FirstOrDefault(c => c.UserId == userId);

            if(cartHeader != null) {
                _context.CartDetails.RemoveRange(
                    _context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id)
                );
                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var cartHeader = _context.CartHeaders.FirstOrDefault(c => c.UserId == userId);

            if (cartHeader != null) {
                cartHeader.CouponCode = couponCode;
                _context.CartHeaders.Update(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            var cartHeader = _context.CartHeaders.FirstOrDefault(c => c.UserId == userId);

            if (cartHeader != null)
            {
                cartHeader.CouponCode = "";
                _context.CartHeaders.Update(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
