using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RShop.DAL.DTO.Requests;
using RShop.DAL.DTO.Responses;

namespace RShop.BLL.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<CheckoutResponse> ProcessPaymentAsync(CheckoutRequest request, string userId,HttpRequest httpRequest);
        Task<bool> HandlePaymentSuccessAsync(int OrderId);
    }
}
