using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.Data;
using RShop.DAL.Models;

namespace RShop.BLL.Services.Classes
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Order> GetUserByOrderAsync(int order)
        {
            var orders = await _context.Orders.Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == order);
            return orders;
        }

    }
}
