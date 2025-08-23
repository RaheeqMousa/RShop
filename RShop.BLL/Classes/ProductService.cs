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
using RShop.BLL.Interfaces;

namespace RShop.BLL.Services.Classes
{
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService
    {
        private readonly IFileService _fileService;
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository, IFileService fileService) : base(repository)
        {
            _fileService = fileService;
            _repository = repository;
        }

        public async Task<int> CreateFile(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            entity.createdAt = DateTime.UtcNow;
            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadFileAsync(request.MainImage);
                entity.MainImage = imagePath;
            }

            return _repository.Add(entity);
        }

    }
}
