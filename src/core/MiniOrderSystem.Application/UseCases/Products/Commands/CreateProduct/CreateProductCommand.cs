using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<Result>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; } = default;
    }
}
