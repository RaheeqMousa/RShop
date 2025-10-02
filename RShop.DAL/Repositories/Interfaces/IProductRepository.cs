using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.Models;

namespace RShop.DAL.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task DecreaseQuantityAsync(List<(int productId, int quantity)> items);
    }
}
