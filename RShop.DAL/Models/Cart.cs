using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RShop.DAL.Models
{
    [PrimaryKey(nameof(ProductId),nameof(UserId))]
    public class Cart :BaseModel
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int count { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
