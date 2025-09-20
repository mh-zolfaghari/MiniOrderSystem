using MiniOrderSystem.Application.Common.Security;
using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Products.Queires.GetProducts
{
    public record GetProductsQuery : IRequest<Result<IEnumerable<ProductDto>>>, IAnonymousRequest
    {
        public string? Name { get; set; } = default;
    }
}
