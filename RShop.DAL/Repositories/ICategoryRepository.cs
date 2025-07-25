using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.Models;

namespace RShop.DAL.Repositories
{
    public interface ICategoryRepository
    {
        int Add(Category category);
        IEnumerable<Category> getAll(bool withTracking=false);
        Category getById(int Id);
        int Remove(Category category);
        int Update(Category category);
        
    }
}
