using MiniOrderSystem.Application.Common;
using MiniOrderSystem.Application.UseCases.Products.Commands.CreateProduct;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.Test.UseCases.Commands.CreateProduct
{
    public class CreateProductCommandHandlerTests
    {
        private readonly IProductRepository _productRepositoryMock;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _productRepositoryMock = Substitute.For<IProductRepository>();
            _handler = new CreateProductCommandHandler(_productRepositoryMock);
        }

        private async Task<Result> ExecuteHandler(CreateProductCommand command, CancellationToken cancellationToken = default)
            => await _handler.Handle(command, cancellationToken);

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenProductIsCreated()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Price = 10.0m,
                Description = "Test Product Description."
            };

            _productRepositoryMock.AddAsync(Arg.Any<Domain.Entities.Product>(), Arg.Any<CancellationToken>()).Returns(Result.Success());

            // Act
            var result = await ExecuteHandler(command);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            await _productRepositoryMock.Received(1).AddAsync(Arg.Any<Domain.Entities.Product>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenDatabaseInsertFails()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Price = 10.0m,
                Description = "Test Product Description."
            };

            _productRepositoryMock.AddAsync(Arg.Any<Domain.Entities.Product>(), Arg.Any<CancellationToken>()).Returns(Result.Failure(CommonMessages.Database.InsertFailed));

            // Act
            var result = await ExecuteHandler(command);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();

            await _productRepositoryMock.Received(1).AddAsync(Arg.Any<Domain.Entities.Product>(), Arg.Any<CancellationToken>());
        }
    }
}
