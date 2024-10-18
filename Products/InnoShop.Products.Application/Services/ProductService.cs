using FluentValidation;
using InnoShop.Products.Application.Interfaces;
using InnoShop.Products.Domain.Entities;
using InnoShop.Products.Domain.Repositories;


namespace InnoShop.Products.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IValidator<Product> productValidator;

    public ProductService(IProductRepository productRepository, IValidator<Product> productValidator)
    {
        this.productRepository = productRepository;
        this.productValidator = productValidator;
    }

    public async Task<Product> CreateProductAsync(string name, string description, decimal price, bool availability, Guid userId)
    {
        var product = new Product(name, description, price, availability, userId);

        await ValidateProductAsync(product);

        await productRepository.CreateAsync(product);
        return product;
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        EnsureProductExists(product);
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await productRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByUserIdAsync(Guid userId)
    {
        return await productRepository.GetByUserIdAsync(userId);
    }

    public async Task UpdateProductAsync(Guid id, string name, string description, decimal price, bool availability, Guid userId)
    {
        var product = await productRepository.GetByIdAsync(id);
        EnsureProductExists(product);
        EnsureUserOwnsProduct(product, userId);

        product.UpdateProduct(name, description, price, availability);
        await ValidateProductAsync(product);

        await productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(Guid id, Guid userId)
    {
        var product = await productRepository.GetByIdAsync(id);
        EnsureProductExists(product);
        EnsureUserOwnsProduct(product, userId);

        await productRepository.DeleteAsync(id);
    }

    private void EnsureProductExists(Product? product)
    {
        if (product == null)
        {
            throw new Exception("Product not found.");
        }
    }

    private void EnsureUserOwnsProduct(Product product, Guid userId)
    {
        if (product.UserId != userId)
        {
            throw new UnauthorizedAccessException("You can only modify your own products.");
        }
    }

    private async Task ValidateProductAsync(Product product)
    {
        var validationResult = await productValidator.ValidateAsync(product);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}