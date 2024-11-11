using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByUserId(string userId, string token);
    }
}
