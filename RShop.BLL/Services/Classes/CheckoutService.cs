using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.DTO.Requests;
using RShop.DAL.DTO.Responses;
using RShop.DAL.Models;
using RShop.DAL.Repositories.Classes;
using RShop.DAL.Repositories.Interfaces;
using Stripe.Checkout;
using Stripe.Climate;

namespace RShop.BLL.Services.Classes
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepository orderIemRepository;
        private readonly IProductRepository productRepository;

        public CheckoutService(ICartRepository cartRepository, IOrderRepository orderRepository,
            IEmailSender emailSender, IOrderItemRepository orderIemRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
            this.orderIemRepository = orderIemRepository;
            this.productRepository = productRepository;
        }

        public async Task<bool> HandlePaymentSuccessAsync(int OrderId)
        {
            var order = await _orderRepository.GetUserByOrderAsync(OrderId); // Await the Task<Order> to get the actual Order object
            var subject = "";
            var body = "";
            if (order == null)
            {
                return false;
            }

            if (order.PaymentMethod == PaymentMethod.Visa)
            {
                subject= "Payment Successful - RShop";
                var carts = await _cartRepository.GetUserCartAsync(order.UserId);
                var orderItems = new List<OrderItems>();
                var productUpdated = new List<(int productId, int quantity)>();
                foreach (var item in carts)
                {
                    var orderItem = new OrderItems
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Count = item.count,
                        Price = item.Product.Price,
                        totalPrice = (double)(item.Product.Price * item.count)
                    };
                    orderItems.Add(orderItem);
                    productUpdated.Add((item.ProductId, item.count));
                }
                await orderIemRepository.AddRangeAsync(orderItems);
                await _cartRepository.ClearCartAsync(order.UserId);
                await productRepository.DecreaseQuantityAsync(productUpdated);

                body = $"<h1>Your payment was successful. Thank you for shopping with us!</h1><p>Your payment for order {OrderId}</p>";
            }
            else if (order.PaymentMethod == PaymentMethod.CashOnDelivery)
            {
                subject = "Order placed successfully - RShop";
                body = $"<h1>Your order was placed successful. Thank you for shopping with us!</h1><p>Your payment for order {OrderId}</p>";
                await _cartRepository.ClearCartAsync(order.UserId);
            }

            await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;
        }

        public async Task<CheckoutResponse> ProcessPaymentAsync(CheckoutRequest request, string userId, HttpRequest HttpRequest)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(userId);
            if (!cartItems.Any())
            {
                return await Task.FromResult(new CheckoutResponse
                {
                    Success = false,
                    Message = "Cart is empty"
                });
            }

            DAL.Models.Order order = new DAL.Models.Order
            {
                UserId = userId,
                PaymentMethod = request.PaymentMethod,
                TotalAmount = (double)cartItems.Sum(i => i.Product.Price * i.count),
                status = OrderStatus.Pending
            };

            await _orderRepository.AddAsync(order);

            if (request.PaymentMethod == PaymentMethod.CashOnDelivery)
            {
                // Handle cash payment logic here
                return await Task.FromResult(new CheckoutResponse
                {
                    Success = true,
                    Message = "Cash payment processed successfully.",
                    OrderId= order.Id
                });
            }

            if (request.PaymentMethod == PaymentMethod.Visa)
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = $"{HttpRequest.Scheme}://{HttpRequest.Host}/api/Customer/Checkout/success/{order.Id}",
                    CancelUrl = $"{HttpRequest.Scheme}://{HttpRequest.Host}/checkout/cancel",
                };

                foreach (var item in cartItems)
                {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                            },
                            UnitAmount = (long)(item.Product.Price*100),
                        },
                        Quantity = item.count,
                    });
                }

                var service = new SessionService();
                var session = service.Create(options);

                order.PaymentId = session.Id;
                await _orderRepository.UpdateAsync(order);

                return new CheckoutResponse()
                {
                    Success = true,
                    Message = "Payment session created successfully.",
                    PaymentId= session.Id,
                    Url = session.Url,
                    OrderId = order.Id
                };
            }

            // Default case if no valid payment method is provided
            return await Task.FromResult(new CheckoutResponse
            {
                Success = false,
                Message = "Invalid payment method."
            });
        }
    }
   }

