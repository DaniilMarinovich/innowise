using InnoShop.Products.API.Models;
using InnoShop.Products.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace InnoShop.Products.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/products
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products.Select(p => new ProductResponse(p)));
    }

    // GET: api/products/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByUserId(Guid userId)
    {
        var products = await _productService.GetProductsByUserIdAsync(userId);
        return Ok(products.Select(p => new ProductResponse(p)));
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProductById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return Ok(new ProductResponse(product));
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductRequest request)
    {
        var userId = GetUserIdFromToken();
        var product = await _productService.CreateProductAsync(request.Name, request.Description, request.Price, request.Availability, userId);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, new ProductResponse(product));
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request)
    {
        var userId = GetUserIdFromToken();
        await _productService.UpdateProductAsync(id, request.Name, request.Description, request.Price, request.Availability, userId);
        return NoContent();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var userId = GetUserIdFromToken();
        await _productService.DeleteProductAsync(id, userId);
        return NoContent();
    }

    private Guid GetUserIdFromToken()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
        return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : throw new UnauthorizedAccessException();
    }
}
