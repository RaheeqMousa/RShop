using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.Models;
using RShop.DAL.Repositories.Interfaces;
using RShop.DAL.Data;

namespace RShop.DAL.Repositories.Classes
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task AddRangeAsync(List<OrderItems> item)
        {
            await _context.OrderItems.AddRangeAsync(item);
            await _context.SaveChangesAsync();
        }
    }
}
