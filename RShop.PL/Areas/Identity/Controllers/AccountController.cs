using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.DAL.DTO.Requests;
using RShop.DAL.DTO.Responses;
using RShop.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using RegisterRequest = RShop.DAL.DTO.Requests.RegisterRequest;
using LoginRequest = RShop.DAL.DTO.Requests.LoginRequest;

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

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var userResponse = await authenticationService.LoginAsync(loginRequest);
            if (userResponse == null)
                return Unauthorized("Invalid username or password");

            return Ok(userResponse);
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

        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var result = await authenticationService.ForgotPassword(request);
            return Ok(result);
        }


        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordRequest request) { 
            var result = await authenticationService.ResetPassword(request);
            if (result)
            {
                return Ok("Password reset successfully.");
            }
            else
            {
                return BadRequest("Failed to reset password. Please check your code and try again.");
            }
        }

    }
}
