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
    public class CustomerController : ControllerBase
    {
        private readonly OrderDbContext _db;
        public CustomerController(OrderDbContext context) => _db = context;
        [HttpGet]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<Customer>> GetCustomer()
        {
            return await _db.Customers.ToListAsync();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCustomerByID(int id)
        {
            var customer = await _db.Customers.FindAsync(id);
            return customer == null ? NotFound() : Ok(customer);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomerByID), new { id = customer.CustomerId }, customer);
        }
    }
}
