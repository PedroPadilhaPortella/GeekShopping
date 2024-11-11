using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        public const string basePath = "api/v1/order";

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
        }

        public async Task<List<Order>> GetOrdersByUserId(string userId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{basePath}/{userId}");
            Console.WriteLine(response.Content.ToString());
            return await response.ReadContentAs<List<Order>>();
        }
    }
}
