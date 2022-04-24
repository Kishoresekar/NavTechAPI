using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NavTechAPI.Data;
using NavTechAPI.Filters;
using NavTechAPI.Models;
using Newtonsoft.Json;
using System.Data;

namespace NavTechAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandling]
    public class OrderController : ControllerBase
    {
        
        private readonly OrderDbContext _db;
        private readonly IConfiguration _configuration;
        public OrderController(OrderDbContext context, IConfiguration configuration)
        {
            _db = context;
            _configuration = configuration;
            
        }

        [HttpGet]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _db.Orders.ToListAsync();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrdersByID(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Order order)
        {
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrdersByID), new { id = order.OrderId }, order);
        }

        [HttpGet("{pageno},{pagesize}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetOrdersByPage(int pageno, int pagesize)
        {
            DataTable table = new DataTable();
            var constr = _configuration.GetConnectionString("SqlServer");
            var con = new SqlConnection(constr);
            var cmd = new SqlCommand("dbo.Ordersdetails", con);
            cmd.Parameters.AddWithValue("@Pageno", pageno);
            cmd.Parameters.AddWithValue("@Pagerow", pagesize);
            cmd.CommandType = CommandType.StoredProcedure;
            var da = new SqlDataAdapter(cmd);
            da.Fill(table);
            var js = JsonConvert.SerializeObject(table);

            
            return new ContentResult { Content = js, ContentType = "application/json" };

        }

        

    }
}
