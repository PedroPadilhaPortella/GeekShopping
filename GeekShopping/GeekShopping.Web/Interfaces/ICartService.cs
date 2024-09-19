using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
    public interface ICartService
    {
        Task<Cart> FindCardByUserId(string userId, string token);
        Task<Cart> AddItemToCart(Cart cart, string token);
        Task<Cart> UpdateCartItem(Cart cart, string token);
        Task<bool> RemoveFromCart(long id, string token);
        Task<bool> ApplyCoupon(Cart cart, string couponCode, string token);
        Task<bool> RemoveCoupon(string userId, string token);
        Task<Cart> ClearCart(string userId, string token);
        Task<Cart> Checkout(CartHeader cartHeader, string token);
    }
}
