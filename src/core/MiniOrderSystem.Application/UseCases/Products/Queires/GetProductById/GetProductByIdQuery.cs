using MiniOrderSystem.Application.Common.Security;
using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Products.Queires.GetProductById
{
    public record GetProductByIdQuery : IRequest<Result<ProductDto>>, IAnonymousRequest
    {
        public required int Id { get; init; }
    }
}
