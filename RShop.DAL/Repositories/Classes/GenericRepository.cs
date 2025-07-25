using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.Data;
using RShop.DAL.Repositories.Interfaces;
using RShop.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace RShop.DAL.Repositories.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDBContext context;

        public GenericRepository(ApplicationDBContext dbcontext)
        {
            context = dbcontext;
        }

        public int Add(T entity)
        {
            context.Set<T>().Add(entity);
            return context.SaveChanges();
        }

        public IEnumerable<T> getAll(bool withTracking = false)
        {
            if (withTracking)
                return context.Set<T>().ToList();
            return context.Set<T>().AsNoTracking().ToList();
        }

        public T? getById(int Id)
        {
            return context.Set<T>().Find(Id);
        }

        public int Remove(T entity)
        {
            context.Set<T>().Remove(entity);
            return context.SaveChanges();
        }

        public int Update(T entity)
        {
            context.Set<T>().Update(entity);
            return context.SaveChanges();
        }
    }
}
