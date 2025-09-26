using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.Models;
using RShop.DAL.Repositories.Classes;

namespace RShop.DAL.Repositories.Interfaces
{
    public interface ICartRepository 
    {
        Task<int> AddAsync(Cart cart);

        Task<List<Cart>> GetUserCartAsync(string UserId);

    }
}
