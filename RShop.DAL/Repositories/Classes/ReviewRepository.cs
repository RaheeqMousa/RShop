using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RShop.DAL.Data;
using RShop.DAL.Models;
using RShop.DAL.Repositories.Interfaces;

namespace RShop.DAL.Repositories.Classes
{
    public class ReviewRepository: IReviewRepository
    {
        private readonly ApplicationDBContext _context;
        public ReviewRepository(ApplicationDBContext context) {

            _context = context;
        }

        public async Task<bool> HasUserReviewedProduct(string userId, int productId) {
            return await _context.Reviews.AnyAsync(r=> r.UserId == userId && r.ProductId == productId);
        }

        public async Task AddReviewAsync(Review request, string userId) { 
            request.UserId = userId;
            request.ReviewDate = DateTime.Now;
            await _context.Reviews.AddAsync(request);
            await _context.SaveChangesAsync();
        }
    }
}
