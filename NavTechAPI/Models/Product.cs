using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace NavTechAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
    }
}
