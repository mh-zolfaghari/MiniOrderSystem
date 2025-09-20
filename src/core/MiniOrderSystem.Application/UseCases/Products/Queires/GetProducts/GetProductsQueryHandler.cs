using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.Helpers;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Products.Queires.GetProducts
{
    public class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProductDto>>>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(request.Name, cancellationToken);
            return Result.Success<IEnumerable<ProductDto>>(products?.Any() != true ? [] : products.Select(x => x.ToDto()!));
        }
    }
}
