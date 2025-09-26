using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RShop.DAL.DTO.Responses
{
    public class CartResponseSummary
    {
        public List<CartResponse> Items { get; set; } = new List<CartResponse>();
        public decimal TotalPrice => Items.Sum(i => i.TotalPrice);
    }
}
