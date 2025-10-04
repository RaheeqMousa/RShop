using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.DTO.Responses;
using RShop.DAL.Models;

namespace RShop.BLL.Services.Classes
{
    public class UserService:IUserService
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
           var users= _userManager.Users.ToList();
            var userDtos = new List<UserDTO>();
            foreach (var user in users)
            {

                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    EmailConfirmed = user.EmailConfirmed,
                    RoleName = roles.FirstOrDefault(),

                });
            }
            return userDtos;
        }

        public async Task<UserDTO> GetByIdAsync(string id)
        {
            var user= await _userService.GetByIdAsync(id);
            return user.Adapt<UserDTO>();
        }

        public async Task<bool> BlockUserAsync(string email, int minutes)
        {
            return await _userService.BlockUserAsync(email, minutes);
        }

        public async Task<bool> UnblockUserAsync(string email)
        {
            return await _userService.UnblockUserAsync(email);
        }
        public async Task<bool> IsBlockedAsync(string userId)
        {
            return await _userService.IsBlockedAsync(userId);
        }

        public async Task<bool> ChangeUserRoleAsync(string userId, string newRole)
        {
           return await _userService.ChangeUserRoleAsync(userId, newRole);
        }
    }
}
