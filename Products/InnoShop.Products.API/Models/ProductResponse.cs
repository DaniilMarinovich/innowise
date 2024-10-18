using InnoShop.Products.Domain.Entities;
using System;

namespace InnoShop.Products.API.Models;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool Availability { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public ProductResponse(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
        Availability = product.IsAvailable;
        UserId = product.UserId;
        CreatedAt = product.CreatedAt;
    }
}
