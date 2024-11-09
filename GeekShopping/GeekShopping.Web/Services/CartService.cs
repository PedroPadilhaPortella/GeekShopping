using GeekShopping.Web.DTO;
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

        public async Task<CartDTO> FindCardByUserId(string userId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{basePath}/{userId}");
            return await response.ReadContentAs<CartDTO>();
        }

        public async Task<CartDTO> AddItemToCart(CartDTO cart, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJson(basePath, cart);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<CartDTO>();
        }

        public async Task<CartDTO> UpdateCartItem(CartDTO cart, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsJson(basePath, cart);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<CartDTO>();
        }

        public async Task<bool> RemoveFromCart(long id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"{basePath}/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<bool>();
        }

        public async Task<CartDTO> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ApplyCoupon(CartDTO cart, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJson($"{basePath}/apply-coupon", cart);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<bool>();
        }

        public async Task<bool> RemoveCoupon(string userId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"{basePath}/remove-coupon/{userId}");
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<bool>();
        }

        public async Task<object> Checkout(CartHeaderDTO cartHeaderDTO, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJson($"{basePath}/checkout", cartHeaderDTO);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartHeader>();
            else if (response.StatusCode.ToString().Equals("PreconditionFailed"))
                return "Coupon Price has changed, please confirm!";
            else
                throw new Exception("Something went wrong");
        }
    }
}
