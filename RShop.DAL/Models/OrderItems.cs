using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RShop.DAL.Models
{
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    public class OrderItems
    {
        public int OrderId { get; set; }

        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double totalPrice { get; set; }
        public int Count { get; set; }

        [Precision(5, 2)]
        public decimal Price { get; set; }


    }
}
