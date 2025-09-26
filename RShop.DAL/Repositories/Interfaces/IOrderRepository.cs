﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RShop.BLL.Services.Interfaces
{
    public interface IOrderRepository
    {
        Task<RShop.DAL.Models.Order> GetUserByOrderAsync(int order);
    }
}
