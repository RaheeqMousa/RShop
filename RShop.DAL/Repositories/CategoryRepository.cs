using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RShop.DAL.Data;
using RShop.DAL.Models;

namespace RShop.DAL.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ApplicationDBContext context;
        public CategoryRepository(ApplicationDBContext dbcontext)
        {
            context = dbcontext;
        }

        public int Add(Category category)
        {
            context.Categories.Add(category);
            return context.SaveChanges();

        }

        public IEnumerable<Category> getAll(bool withTracking = false)
        {
            if (withTracking) 
                return context.Categories.ToList();
            return context.Categories.AsNoTracking().ToList();
        }

        public Category? getById(int Id)
        {
            return context.Categories.Find(Id);
        }

        public int Remove(Category category)
        {
            context.Categories.Remove(category);
            return context.SaveChanges();
        }

        public int Update(Category category)
        {
            context.Categories.Update(category);
            return context.SaveChanges();
        }
    }
}
