using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.DAL.DTO.Requests;
using RShop.DAL.DTO.Responses;
using RShop.BLL.Services.Interfaces;

namespace RShop.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly RShop.BLL.Services.Interfaces.IAuthenticationService authenticationService;

        public AccountController(RShop.BLL.Services.Interfaces.IAuthenticationService authService)
        {
           this.authenticationService = authService;
        }

        [HttpPost("register")]
        //or return Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
        public async Task<ActionResult<UserResponse>> RegisterAsync([FromBody] RegisterRequest registerRequest)
        {
            var userResponse = await authenticationService.RegisterAsync(registerRequest);
            return Ok(userResponse);
        }

        [HttpGet("ConfirmEmail")]
        //"https://localhost:7216/api/identity/Account/ConfirmEmail?token={escapeToken}&userId={user.Id}"
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string token, [FromQuery] string userId)
        {
            var userResponse = await authenticationService.ConfirmEmail(token,userId);
            return Ok(userResponse);
        }
    }
}
