namespace InnoShop.Products.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool IsAvailable { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; } 

    public Product(string name, string description, decimal price, bool isAvailable, Guid userId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        IsAvailable = isAvailable;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateProduct(string name, string description, decimal price, bool isAvailable)
    {
        Name = name;
        Description = description;
        Price = price;
        IsAvailable = isAvailable;
    }
}
