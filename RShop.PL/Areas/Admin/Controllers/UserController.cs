using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.DTO.Requests;

namespace RShop.PL.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("block/{userId}")]
        public async Task<IActionResult> BlockUserAsync([FromRoute] string id, [FromBody] int minutes)
        {
            var result = await _userService.BlockUserAsync(id, minutes);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpPost("unblock/{userId}")]
        public async Task<IActionResult> UnblockUserAsync([FromRoute] string id)
        {
            var result = await _userService.UnblockUserAsync(id);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpGet("isblocked/{userId}")]
        public async Task<IActionResult> IsBlockedAsync([FromRoute]string userId)
        {
            var result = await _userService.IsBlockedAsync(userId);
            return Ok(result);
        }

        [HttpPatch("changerole/{userId}")]
        public async Task<IActionResult> ChangeUserRoleAsync([FromRoute] string userId, [FromBody] ChangeRoleDTO changeRole)
        {
            var result = await _userService.ChangeUserRoleAsync(userId, changeRole.RoleName);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
