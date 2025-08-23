using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RShop.DAL.DTO.Responses;
using RShop.DAL.DTO.Requests;
using Microsoft.AspNetCore.Identity.Data;


namespace RShop.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(DAL.DTO.Requests.LoginRequest loginRequest);

        Task<UserResponse> RegisterAsync(DAL.DTO.Requests.RegisterRequest registerRequest);

        Task<string> ConfirmEmail(string token, string userId);

        Task<bool> ForgotPassword(ForgotPasswordRequest request);

        Task<bool> ResetPassword(ResetPasswordRequest request);
    }
}

