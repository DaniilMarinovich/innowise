using Microsoft.EntityFrameworkCore;
using InnoShop.Products.Domain.Entities;

namespace InnoShop.Products.Infrastructure.DBContexts;

public class ProductContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
