using GeekShopping.CartAPI.DTO;
using GeekShopping.CartAPI.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.CartAPI.Repository
{
    public class CouponRepository: ICouponRepository
    {
        private readonly HttpClient _httpClient;
        public const string basePath = "api/v1/coupon";

        public CouponRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
        }

        public async Task<CouponDTO> GetCoupon(string couponCode, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{basePath}/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK) return new CouponDTO();

            return JsonSerializer.Deserialize<CouponDTO>(content, 
                new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        }
    }
}
