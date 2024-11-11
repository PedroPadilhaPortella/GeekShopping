using GeekShopping.Web.DTO;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderHeaderDTO> AddOrder(OrderHeader orderHeader);
        Task UpdateOrderPaymentStatus(long orderHeaderId, bool status);
        Task<List<OrderHeaderDTO>> GetOrders(string userId);
    }
}
