using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        public const string basePath = "api/v1/cart";

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
        }

        public async Task<Cart> FindCardByUserId(string userId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{basePath}/{userId}");
            return await response.ReadContentAs<Cart>();
        }

        public async Task<Cart> AddItemToCart(Cart cart, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJson(basePath, cart);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<Cart>();
        }

        public async Task<Cart> UpdateCartItem(Cart cart, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsJson(basePath, cart);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<Cart>();
        }

        public async Task<bool> RemoveFromCart(long id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"{basePath}/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<bool>();
        }

        public async Task<Cart> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ApplyCoupon(Cart cart, string couponCode, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCoupon(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> Checkout(CartHeader cartHeader, string token)
        {
            throw new NotImplementedException();
        }
    }
}
