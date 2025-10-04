using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RShop.DAL.Models;

namespace RShop.DAL.DTO.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        [Precision(5, 2)]
        public decimal discount { get; set; }
        public int quantity { get; set; }
        public double Rating { get; set; }
        public IFormFile MainImage { get; set; }
        public List<IFormFile> SubImages { get; set; }

        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}
