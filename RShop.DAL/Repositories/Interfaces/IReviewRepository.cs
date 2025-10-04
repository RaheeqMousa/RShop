using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.Models;
using RShop.DAL.Repositories.Classes;

namespace RShop.DAL.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        public Task<bool> HasUserReviewedProduct(string userId, int productId);
        public Task AddReviewAsync(Review request, string userId);
    }
}
