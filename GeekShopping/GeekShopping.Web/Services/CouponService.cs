using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using System.Net;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _httpClient;
        public const string basePath = "api/v1/coupon";

        public CouponService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
        }

        public async Task<IEnumerable<Coupon>> GetCoupons(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(basePath);
            return await response.ReadContentAs<List<Coupon>>();
        }

        public async Task<Coupon> GetCoupon(long id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{basePath}/{id}");
            if (response.StatusCode != HttpStatusCode.OK) return new Coupon();
            return await response.ReadContentAs<Coupon>();
        }

        public async Task<Coupon> GetCoupon(string code, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{basePath}/{code}/code");
            if (response.StatusCode != HttpStatusCode.OK) return new Coupon();
            return await response.ReadContentAs<Coupon>();
        }

        public async Task<Coupon> Create(Coupon coupon, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJson(basePath, coupon);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<Coupon>();
        }

        public async Task<Coupon> Update(Coupon coupon, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsJson(basePath, coupon);
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<Coupon>();
        }

        public async Task<bool> Delete(long id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"{basePath}/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Something went wrong");
            return await response.ReadContentAs<bool>();
        }
    }
}
