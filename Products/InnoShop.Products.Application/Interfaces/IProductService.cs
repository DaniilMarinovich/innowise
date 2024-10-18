using InnoShop.Products.Domain.Entities;

namespace InnoShop.Products.Application.Interfaces;

public interface IProductService
{
    Task<Product> CreateProductAsync(string name, string description, decimal price, bool availability, Guid userId);
    Task<Product> GetProductByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetProductsByUserIdAsync(Guid userId);
    Task UpdateProductAsync(Guid id, string name, string description, decimal price, bool availability, Guid userId);
    Task DeleteProductAsync(Guid id, Guid userId);
}
