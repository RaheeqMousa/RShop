using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.DTO.Requests;
using RShop.DAL.DTO.Responses;
using RShop.DAL.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.Data;


namespace RShop.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailSender emailSender)
        {
            _configuration = configuration;
            _userManager = userManager;
            _emailSender = emailSender;

        }

        public async Task<UserResponse> LoginAsync(DAL.DTO.Requests.LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                throw new Exception("Invalid Email or Password");
            }

            if (!await _userManager.IsEmailConfirmedAsync(user)) {
                throw new Exception("Email is not confirmed. Please check your email for confirmation link.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (isPasswordValid)
            {
                return new UserResponse
                {
                    Token = await CreateTokenAsync(user)
                };
            }

            
            throw new Exception("Invalid Email or Password");
        }

        public async Task<string> ConfirmEmail(string token, string userId) {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) {
                throw new Exception("user not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email confirmed successfully.";
            }
            else
            {
                return "Email confrimation failed";
            }
        }

        public async Task<UserResponse> RegisterAsync(RShop.DAL.DTO.Requests.RegisterRequest registerRequest)
        {
            var user = new ApplicationUser()
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
                PhoneNumber = registerRequest.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (result.Succeeded)
            {
                // Generate email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = Uri.EscapeDataString(token);

                // Create confirmation URL
                var emailUrl = $"https://localhost:7216/api/identity/Account/ConfirmEmail?token={encodedToken}&userId={user.Id}";

                // Send confirmation email
                await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                    $"<h1>Hello {user.UserName}</h1>" +
                    $"<p>Click <a href='{emailUrl}'>here</a> to confirm your email.</p>");

                return new UserResponse
                {
                    Token = registerRequest.Email 
                };
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim("Email", user.Email),
                new Claim("Name", user.UserName),
                new Claim("Id", user.Id)
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKeyString = _configuration["jwtOptions:SecretKey"];

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString));
            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "RShop",
                audience: "RShop",
                claims: Claims,
                expires: DateTime.Now.AddDays(30), // Token valid for 30 days
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var random = new Random();
            var code = random.Next(1000, 9999).ToString();
            user.CodeResetPassword = code;
            user.PasswordResetCodeExpiry = DateTime.UtcNow.AddMinutes(15); // Code valid for 15 minutes
            //UtcNow is used to ensure the code is valid across different time zones

            await _userManager.UpdateAsync(user);
            // Send email with the code
            await _emailSender.SendEmailAsync(user.Email, "Reset Password",
                $"<h1>Hello {user.UserName}</h1>" +
                $"<p>Your reset password code is: <strong>{code}</strong></p>" +
                $"<p>This code is valid for 15 minutes.</p>");

            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request) { 
            var user=_userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return false;
            }
            if(user.Result.CodeResetPassword != request.ResetCode)
            {
                return false;
            }
            if (user.Result.PasswordResetCodeExpiry < DateTime.UtcNow)
            {
                return false;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user.Result);
            var resetResult= await _userManager.ResetPasswordAsync(user.Result,token, request.NewPassword);
            if (resetResult.Succeeded) {
                await _emailSender.SendEmailAsync(request.Email, "Password Reset Successful",
                    $"<h1>Hello {user.Result.UserName}</h1>" +
                    $"<p>Your password has been reset successfully.</p>");
            }

            return true;
        }
    }
    
}
