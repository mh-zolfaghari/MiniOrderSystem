using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.Helpers;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Products.Queires.GetProductById
{
    public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var foundedProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            return foundedProduct is null
                ? Result.Failure<ProductDto>(ProductMessages.NotFound)
                : Result.Success(foundedProduct.ToDto()!);
        }
    }
}
