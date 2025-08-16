using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.DTO.Requests;

namespace RShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            try
            {
                var category = categoryService.GetById(id);
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] CategoryRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid category data.");
            }
            var id = categoryService.Create(request);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoryRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid category data.");
            }
            try
            {
                var updatedId = categoryService.Update(id, request);
                return Ok(updatedId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deletedId = categoryService.Delete(id);
                return Ok(deletedId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}/toggle-status")]
        public IActionResult ToggleStatus(int id)
        {
            try
            {
                var updated = categoryService.ToggleStatus(id);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
