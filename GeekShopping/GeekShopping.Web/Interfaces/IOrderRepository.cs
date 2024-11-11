using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderHeader> AddOrder(OrderHeader orderHeader);
        Task UpdateOrderPaymentStatus(long orderHeaderId, bool status);
    }
}
