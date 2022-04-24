using Microsoft.EntityFrameworkCore;
using NavTechAPI.Models;

namespace NavTechAPI.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasAlternateKey(c => c.CustomerEmail)
                .HasName("AlternateKey_CustomerEmail");
            
        }
        
    }
}
