using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RShop.DAL.DTO.Responses;

namespace RShop.DAL.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        [Precision(5,2)]
        public decimal discount { get; set; }
        public int quantity { get; set; }
        public double Rating { get; set; }
        public string MainImage { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? BrandId { get; set; }
        public Brand? brand { get; set; }

        public List<ProductImage> SubImages { get; set; } = new List<ProductImage>();

        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();
    }
}
