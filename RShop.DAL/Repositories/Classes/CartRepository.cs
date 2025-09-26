using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RShop.DAL.Data;
using RShop.DAL.Models;
using RShop.DAL.Repositories.Interfaces;

namespace RShop.DAL.Repositories.Classes
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDBContext _context;
        public CartRepository(ApplicationDBContext context) {
            _context= context;
        }
        public async Task<int> AddAsync(Cart cart) { 
            await _context.Carts.AddAsync(cart);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetUserCartAsync(string UserId)
        {

            return await _context.Carts.Include(c=>c.Product).Where(c => c.UserId == UserId).ToListAsync();
        }
    }
}
