using Microsoft.EntityFrameworkCore;
using GeekShopping.Web.Models;
using GeekShopping.Web.Data;
using GeekShopping.Web.Interfaces;
using AutoMapper;
using GeekShopping.Web.DTO;

namespace GeekShopping.Web.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderHeaderDTO> AddOrder(OrderHeader header)
        {
            if (header == null) return null;

             _context.OrderHeaders.Add(header);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderHeaderDTO>(header);
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool status)
        {
            var header = await _context.OrderHeaders.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
            
            if (header != null) {
                header.PaymentStatus = status;
                await _context.SaveChangesAsync();
            };
        }

        public async Task<List<OrderHeaderDTO>> GetOrders(string userId)
        {
            var orders = await _context.OrderHeaders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails).ToListAsync();

            return _mapper.Map<List<OrderHeaderDTO>>(orders);
        }
    }
}
