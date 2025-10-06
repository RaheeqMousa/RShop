using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.DAL.DTO.Requests;

namespace RShop.PL.Areas.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles ="Customer")]
    public class ReviewsController : ControllerBase
    {
        public ReviewsController() { }

        [HttpPost("")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest reviewRequest) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //User is the current authenticated user
            return Ok();
        }
    }
}
