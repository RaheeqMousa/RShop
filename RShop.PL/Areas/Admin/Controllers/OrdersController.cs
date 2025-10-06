using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.BLL.Services.Classes;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.Models;

namespace RShop.PL.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus([FromRoute] int status)
        {
            var orders = await _orderService.GetByStatusAsync((DAL.Models.OrderStatus)status);
            return Ok(orders);
        }

        [HttpPatch("changestatus/{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus([FromRoute] int orderId, [FromBody] OrderStatus status)
        {
            var result = await _orderService.ChangeStatusAsync(orderId, (DAL.Models.OrderStatus)status);
            if (!result) return NotFound();
            return Ok(new { message="status is changed"});
        }

    }
}
