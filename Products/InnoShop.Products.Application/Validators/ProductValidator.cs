using FluentValidation;
using InnoShop.Products.Domain.Entities;

namespace InnoShop.Products.Application.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .Length(2, 100);

        RuleFor(product => product.Description)
            .NotEmpty()
            .Length(10, 1000);

        RuleFor(product => product.Price)
            .GreaterThan(0);

        RuleFor(product => product.IsAvailable)
            .NotNull();
    }
}
