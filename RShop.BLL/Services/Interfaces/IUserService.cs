using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.DTO.Responses;

namespace RShop.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(string id);

        Task<bool> BlockUserAsync(string email, int minutes);
        Task<bool> UnblockUserAsync(string email);
        Task<bool> IsBlockedAsync(string userId);
        Task<bool> ChangeUserRoleAsync(string userId, string newRole);
    }
}
