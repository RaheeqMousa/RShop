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

        public async Task<Order> GetUserByOrderAsync(int orderId)
        {
            var order = await _context.Orders
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == orderId);
            return order;
        }

        public async Task<Order?> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }


        public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await _context.Orders.Where(
                o => o.status == status
                ). OrderByDescending(O=> O.OrderDate).ToListAsync();
        }


        public async Task<List<Order>> GetAllWithUserAsync(string userId) {

            return await _context.Orders.Where(o=> o.UserId == userId).ToListAsync();

        }

        public async Task<List<Order>> GetOrderByUserAsync(string userId) { 
            return await _context.Orders.Include(o => o.User).OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus status) { 
            var order= await _context.Orders.FindAsync(orderId);
            if (order == null) return false;
            order.status = status;
            var result =await _context.SaveChangesAsync();
            return result>0;
        }

        public async Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId) {
            return await _context.Orders.Include(o => o.OrderItems)
                .AnyAsync(e=> e.UserId == userId && e.status == OrderStatus.Approved &&
                e.OrderItems.Any(orderitem=> orderitem.ProductId==productId));
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
