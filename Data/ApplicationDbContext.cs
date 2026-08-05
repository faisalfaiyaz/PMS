using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products
    {
        get
        {
            return Set<Product>();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Product> products = new List<Product>()
        {
            new Product
            {
                ProductId = 1,
                Name = "Fridge",
                Description ="Refridgerator",
                Category = "Home Appliances",
                CreatedDate = new DateTime(2026, 7, 1),
                ImageUrl = "www.example.com",
                IsActive = true,    
                Price = 80000,
                Quantity = 2
            },
            new Product
            {
                ProductId = 2,
                Name = "iPhone",
                Description ="Mobile Phone",
                Category = "Electronics",
                CreatedDate = new DateTime(2024, 11, 03),
                ImageUrl = "www.something.com",
                IsActive = true,
                Price = 1000,
                Quantity = 20
            }
        };
        
        modelBuilder.Entity<Product>().HasData(products);
    }
}
