using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RShop.DAL.DTO.Requests;
using RShop.DAL.DTO.Responses;

namespace RShop.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        int CreateCategory(CategoryRequest request);
        IEnumerable<CategoryResponse> GetAllCategories();
        CategoryResponse GetCategoryById(int id);
        int UpdateCategory(int id, CategoryRequest request);
        int DeleteCategory(int id);
        bool ToggleStatus(int id);

    }
}
