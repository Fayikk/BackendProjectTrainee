using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MulakatCalisma.DTO;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;
using System.Security.Claims;

namespace MulakatCalisma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Order>>> AddOrders(Order order)
        {

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user!=null)
            {
                order.UserId = int.Parse(user);
                var result = await _orderService.CreateOrder(order);
                return Ok(result);
            }
            return BadRequest("Ooops Fail");
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Order>>>> GetProductbyUser(int userId)
        {
            var result = await _orderService.GetProductByUser(userId);
            return Ok(result);
        }
    }
}
