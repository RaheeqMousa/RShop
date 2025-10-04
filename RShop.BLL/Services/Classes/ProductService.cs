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
using Azure.Core;
using Microsoft.AspNetCore.Http;

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

            if (request.SubImages != null)
            {
                var subImagesPath = await _fileService.UploadManyAsync(request.SubImages);
                entity.SubImages = subImagesPath.Select(img => new ProductImage { ImageName=img}).ToList();
            }

            return _repository.Add(entity);
        }

        public async Task<List<ProductResponse>> GetAllProducts(HttpRequest request, bool onlyActive = false, int pageNumber = 1, int pageSize = 1) {
            var products = _repository.getAllProductsWithImage();

            if (onlyActive) { 
                products= products.Where(products=>products.Status== Status.Active).ToList();
            }


            var pagedCount = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.quantity,
                MainImageUrl = $"{request.Scheme}://{request.Host}/Images/{p.MainImage}",
                SubImagesUrls = p.SubImages.Select(img => $"{request.Scheme}://{request.Host}/${p.MainImage}").ToList(),
                Reviews = p.Reviews.Select(r=> new ReviewResponse { 
                    Id = r.Id,
                    Rate = r.Rate,
                    Comment = r.Comment,
                    FullName = r.FullName
                }).ToList()
            }).ToList();

        }

    }
}
