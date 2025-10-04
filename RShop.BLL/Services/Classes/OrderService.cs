using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.Models;

namespace RShop.BLL.Services.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order?> AddOrderAsync(Order order)
        {
            return await _orderRepository.AddAsync(order);
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus status)
        {
            return await _orderRepository.ChangeStatusAsync(orderId, status);
        }

        public async Task<List<Order>> GetAllWithUserAsync(string userId)
        {
            return await _orderRepository.GetAllWithUserAsync(userId);
        }

        public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await _orderRepository.GetByStatusAsync(status);
        }

        public async Task<List<Order>> GetOrderByUserAsync(string userId)
        {
            return await _orderRepository.GetOrderByUserAsync(userId);
        }

        public async Task<Order?> GetUserByOrderAsync(int order)
        {
            return await _orderRepository.GetUserByOrderAsync(order);
        }
    }
}
