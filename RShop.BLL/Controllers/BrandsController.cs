using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.DTO.Requests;

namespace RShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService brandService;
        public BrandsController(IBrandService bService)
        {
            brandService = bService;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = brandService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var category = brandService.GetById(id);
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] BrandRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid category data.");
            }
            var id = brandService.Create(request);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BrandRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid category data.");
            }
            try
            {
                var updatedId = brandService.Update(id, request);
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
                var deletedId = brandService.Delete(id);
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
                var updated = brandService.ToggleStatus(id);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
