using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.Models;

namespace RShop.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T: BaseModel
    {
        T Add(T entity);
        IEnumerable<T> getAll(bool withTracking = false);
        T? getById(int Id);
        int Update(T entity);
        int Remove(T entity);
        bool ToggleStatus(int id);

    }
}
