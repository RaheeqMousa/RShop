using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.BLL.Services.Interfaces;

namespace RShop.PL.Areas.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] RShop.DAL.DTO.Requests.CartRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.AddToCartAsync(request, userId);

            return result ? Ok() : BadRequest();

        }

        [HttpGet("")]
        public async Task<IActionResult> GetUserCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.CartResponseSummaryAsync(userId);
            return Ok(result);
        }

    }
}
