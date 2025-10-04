using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RShop.DAL.Data;
using RShop.DAL.Models;
using RShop.DAL.Repositories.Interfaces;
using RShop.DAL.Repositories.Classes;

namespace RShop.DAL.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        private readonly ApplicationDBContext _context;

        public ProductRepository(ApplicationDBContext dbcontext):base(dbcontext)
        {
            _context = dbcontext;
        }

        public async Task DecreaseQuantityAsync(List<(int productId, int quantity)> items)
        {
            var productIds = items.Select(i => i.productId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
            foreach(var product in products)
            {
                var item = items.First(i=> i.productId==product.Id);
                    if (product.quantity < item.quantity)
                    {
                        throw new Exception($"Not enough quantity in stock for product {product.Name}.");
                    }
                    product.quantity -= item.quantity;
            }

            await _context.SaveChangesAsync();
        }

        public List<Product> getAllProductsWithImage() { 
            return _context.Products.Include(p=> p.SubImages).Include(p=> p.Reviews).ThenInclude(r=> r.User).ToList();
        }

    }
}
