using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.DTO.Requests;

namespace RShop.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles="Customer")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService) { 
            _checkoutService = checkoutService;
        }

        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody] CheckoutRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _checkoutService.ProcessPaymentAsync(request, userId, Request);

            return Ok(response);
        }

        [HttpGet("Success")]
        [AllowAnonymous]
        public async Task<IActionResult> success([FromRoute] int orderId)
        {
            bool result = await _checkoutService.HandlePaymentSuccessAsync(orderId);
            return Ok(result);
        }

    }
}
