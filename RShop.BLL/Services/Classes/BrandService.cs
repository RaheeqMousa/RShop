using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.DTO.Requests;
using RShop.DAL.DTO.Responses;
using Mapster;
using RShop.DAL.Models;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.Repositories.Interfaces;

namespace RShop.BLL.Services.Classes
{
    public class BrandService : GenericService<BrandRequest, BrandResponse, Brand>, IBrandService
    {
        public BrandService(IBrandRepository request) : base(request)
        {

        }

    }
}
