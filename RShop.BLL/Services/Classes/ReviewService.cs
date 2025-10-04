using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.DTO.Requests;
using RShop.DAL.Models;
using RShop.DAL.Repositories.Interfaces;

namespace RShop.BLL.Services.Classes
{
    public class ReviewService:IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository, IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> AddReviewAsync(ReviewRequest reviewRequest, string userId)
        {
            var hasOrder = await _orderRepository.UserHasApprovedOrderForProductAsync(userId, reviewRequest.ProductId);
            if (!hasOrder)
            {
                return false;
            }

            var reviewedBefore = await _reviewRepository.HasUserReviewedProduct(userId, reviewRequest.ProductId);
            if (reviewedBefore) {
                return false;
            }

            var review = reviewedBefore.Adapt<Review>();
            await _reviewRepository.AddReviewAsync(review, userId);

            return true;
        }
    }
}
