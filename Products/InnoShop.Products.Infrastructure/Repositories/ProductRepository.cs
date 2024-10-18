using InnoShop.Products.Domain.Entities;
using InnoShop.Products.Domain.Repositories;
using InnoShop.Products.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.Products.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext context;

    public ProductRepository(ProductContext context)
    {
        this.context = context;
    }

    //Create Product
    public async Task CreateAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product != null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetByUserIdAsync(Guid userId)
    {
        return await context.Products
                             .AsNoTracking()
                             .Where(p => p.UserId == userId)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await context.Products.ToListAsync();
    }
}
