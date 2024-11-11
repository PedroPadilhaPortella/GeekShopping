using AutoMapper;
using GeekShopping.OrderAPI.DTO;
using GeekShopping.OrderAPI.Models;
using GeekShopping.OrderAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.OrderAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(OrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<OrderHeaderDTO>>> GetOrdersById(string id)
        {
            var orders = await _orderRepository.GetOrdersByUserId(id);
            return Ok(_mapper.Map<List<OrderHeaderDTO>>(orders));
        }
    }
}
