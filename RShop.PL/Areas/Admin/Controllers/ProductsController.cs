using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.BLL.Services.Interfaces;
using RShop.DAL.DTO.Requests;

namespace RShop.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Admin/Products
        // Retrieves all products.
        [HttpGet]
        public IActionResult GetAll() => Ok(_productService.GetAll());

        
        // GET: api/Admin/Products/{id}
        // Retrieves a specific product by ID.
        // Returns 404 if the product is not found.
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var product = _productService.GetById(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        

        // POST: api/Admin/Products
        // Creates a new product.
        // Note: ProductRequest includes IFormFile for image upload, so [FromForm] is used instead of [FromBody].
        [HttpPost("")]
        public IActionResult Create([FromForm] ProductRequest request)
        {
            var result = _productService.CreateFile(request);
            return Ok(result);
        }

        
        // PUT: api/Admin/Products/{id}
        // Updates an existing product.
        // Returns 400 if the request is null, 404 if the product is not found.
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid product data.");
            }
            try
            {
                var updatedId = _productService.Update(id, request);
                return Ok(updatedId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Admin/Products/{id}
        // Deletes a product by ID.
        // Returns 404 if the product is not found.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deletedId = _productService.Delete(id);
                return Ok(deletedId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
    }
}
