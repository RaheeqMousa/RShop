using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.Models;

namespace RShop.DAL.DTO.Requests
{
    public class CheckoutRequest
    {
        public PaymentMethod PaymentMethod { get; set; }
    }
}
