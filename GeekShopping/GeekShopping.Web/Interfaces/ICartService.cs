using GeekShopping.Web.DTO;

namespace GeekShopping.Web.Interfaces
{
    public interface ICartService
    {
        Task<CartDTO> FindCardByUserId(string userId, string token);
        Task<CartDTO> AddItemToCart(CartDTO cart, string token);
        Task<CartDTO> UpdateCartItem(CartDTO cart, string token);
        Task<bool> RemoveFromCart(long id, string token);
        Task<bool> ApplyCoupon(CartDTO cart, string token);
        Task<bool> RemoveCoupon(string userId, string token);
        Task<CartDTO> ClearCart(string userId, string token);
        Task<object> Checkout(CartHeaderDTO cartHeader, string token);
    }
}
