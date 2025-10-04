using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RShop.BLL.Interfaces;
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
        private readonly IFileService _fileService;

        public ProductsController(IProductService productService, IFileService fileService)
        {
            _productService = productService;
            _fileService = fileService;
        }

        // GET: api/Admin/Products
        [HttpGet("")]
        public IActionResult GetAll([FromQuery] int pageNumber=1, [FromQuery] int pageSize=5) {
            return Ok(_productService.GetAllProducts(Request, false, pageNumber, pageSize));
        }

       
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
        // Note: ProductRequest includes IFormFile for image upload
        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            var result = _productService.CreateFile(request);
            if (request.MainImage != null) { 
                var imagePath= await _fileService.UploadFileAsync(request.MainImage);
            }
            return Ok(result);
        }

        
        // PUT: api/Admin/Products/{id}
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
