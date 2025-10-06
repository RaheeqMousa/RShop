using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.Models;

namespace RShop.BLL.Services.Interfaces
{
    public interface IOrderRepository
    {
        Task<RShop.DAL.Models.Order?> GetUserByOrderAsync(int order);
        Task<RShop.DAL.Models.Order?> AddAsync(Order order);
        Task<List<Order>> GetAllWithUserAsync(string userId);
        Task<List<Order>> GetByStatusAsync(OrderStatus status);
        Task<bool> ChangeStatusAsync(int orderId, OrderStatus status);
        Task<List<Order>> GetOrderByUserAsync(string userId);
        Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId);

        Task UpdateAsync(Order order);

    }
}
