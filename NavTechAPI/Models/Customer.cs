using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace NavTechAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        [Required]
        public string CustomerEmail { get; set; }

    }

}
