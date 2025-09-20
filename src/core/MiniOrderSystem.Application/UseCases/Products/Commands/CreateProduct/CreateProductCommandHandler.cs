using MiniOrderSystem.Application.Common;
using MiniOrderSystem.Application.Helpers;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = request.ToModel();
            var dbResult = await _productRepository.AddAsync(newProduct, cancellationToken);

            return dbResult.IsFailure
                ? Result.Failure(CommonMessages.Database.InsertFailed)
                : Result.Success();
        }
    }
}
