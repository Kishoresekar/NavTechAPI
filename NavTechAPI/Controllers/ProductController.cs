using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NavTechAPI.Data;
using NavTechAPI.Filters;
using NavTechAPI.Models;

namespace NavTechAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandling]
    public class ProductController : ControllerBase
    {
        private readonly OrderDbContext _db;
        public ProductController(OrderDbContext context) => _db = context;

        [HttpGet]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductByID(int id)
        {
            var product = await _db.Products.FindAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductByID), new { id = product.ProductId }, product);
        }
    }
}
