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

        public T Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
            return entity;
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

        public bool ToggleStatus(int id)
        {

            var entity = context.Set<T>().Find(id);
            if (entity == null)
            {
                return false; // Entity not found
            }

            entity.Status = (entity.Status == Status.Active) ? Status.Inactive : Status.Active;

            context.Set<T>().Update(entity);
            context.SaveChanges();
            return true; // Status toggled successfully
        }

        public int Update(T entity)
        {
            context.Set<T>().Update(entity);
            return context.SaveChanges();
        }
    }
}
