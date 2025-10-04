using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RShop.DAL.DTO.Requests;
using RShop.DAL.DTO.Responses;
using RShop.DAL.Models;

namespace RShop.BLL.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductRequest, ProductResponse, Brand>
    {
        Task<int> CreateFile(ProductRequest request);
        Task<List<ProductResponse>> GetAllProducts(HttpRequest request, bool onlyActive = false, int pageNumber = 1, int pageSize = 1);

    }
}
