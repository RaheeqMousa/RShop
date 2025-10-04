using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.DTO.Requests;

namespace RShop.BLL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(ReviewRequest reviewRequest, string userId);
    }
}
