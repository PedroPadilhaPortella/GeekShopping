using Microsoft.EntityFrameworkCore;
using GeekShopping.Web.Models;
using GeekShopping.Web.Data;
using GeekShopping.Web.Interfaces;

namespace GeekShopping.Web.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderHeader> AddOrder(OrderHeader header)
        {
            if (header == null) return null;

             _context.OrderHeaders.Add(header);
            await _context.SaveChangesAsync();
            return header;
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool status)
        {
            var header = await _context.OrderHeaders.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
            
            if (header != null) {
                header.PaymentStatus = status;
                await _context.SaveChangesAsync();
            };
        }
    }
}
