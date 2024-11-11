using GeekShopping.OrderAPI.Models;

namespace GeekShopping.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader orderHeader);
        Task UpdateOrderPaymentStatus(long orderHeaderId, bool status);
        Task<List<OrderHeader>> GetOrdersByUserId(string userId);
    }
}
