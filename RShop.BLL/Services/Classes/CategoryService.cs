using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.DTO.Requests;
using RShop.DAL.Repositories;
using RShop.DAL.DTO.Responses;
using Mapster;
using RShop.DAL.Models;

namespace RShop.BLL.Services.Classes
{

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository request)
        {
            categoryRepository = request;
        }

        public int CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            return categoryRepository.Add(category);
        }

        public IEnumerable<CategoryResponse> GetAllCategories()
        {
            var categories = categoryRepository.getAll();
            return categories.Adapt<IEnumerable<CategoryResponse>>();
        }

        public CategoryResponse GetCategoryById(int id)
        {
            var cat=categoryRepository.getById(id);
            if (cat is null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return cat.Adapt<CategoryResponse>();
        }

        public int UpdateCategory(int id, CategoryRequest request)
        {
            var cat = categoryRepository.getById(id);
            if (cat == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }

            var updatedCategory = request.Adapt<Category>();
            updatedCategory.Id = id; // Ensure the ID remains the same
            return categoryRepository.Update(updatedCategory);
        }

        public bool ToggleStatus(int id) // Fixed naming to PascalCase
        {
            var category = categoryRepository.getById(id);
            if (category is null)
            {
                return false;
            }

            category.Status = category.Status == Status.Active ? Status.Inactive : Status.Active;
            categoryRepository.Update(category);
            return true; 
        }

        public int DeleteCategory(int id)
        {
            var category = categoryRepository.getById(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return categoryRepository.Remove(category);
        }
    }
}
