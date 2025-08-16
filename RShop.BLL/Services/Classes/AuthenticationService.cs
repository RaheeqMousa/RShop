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

        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
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

        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest)
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
                Claims.Add(new Claim("Role", role));
            }

            var secretKeyString = _configuration["SecretKey"];

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
    }
    
}
