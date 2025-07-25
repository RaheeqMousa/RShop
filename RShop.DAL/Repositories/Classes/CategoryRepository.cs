﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RShop.DAL.Data;
using RShop.DAL.Models;
using RShop.DAL.Repositories.Interfaces;
using RShop.DAL.Repositories.Classes;

namespace RShop.DAL.Repositories.Classes
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(ApplicationDBContext dbcontext):base(dbcontext)
        {

        }
    }
}
