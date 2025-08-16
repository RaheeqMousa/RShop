using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RShop.DAL.DTO.Responses;
using RShop.DAL.DTO.Requests;

namespace RShop.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest loginRequest);

        Task<UserResponse> RegisterAsync(RegisterRequest registerRequest);

        Task<string> ConfirmEmail(string token, string userId);

    }
}
