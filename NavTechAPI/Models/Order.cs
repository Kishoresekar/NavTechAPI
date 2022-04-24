using System.ComponentModel.DataAnnotations;
namespace NavTechAPI.Models
{
    public class Order
    {

        public int OrderId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public DateTime PurchasedDate { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public int Quantity { get; set; }
    }

    public enum PaymentMethod
    {
        cash, onlinepayment
    }

    
}

