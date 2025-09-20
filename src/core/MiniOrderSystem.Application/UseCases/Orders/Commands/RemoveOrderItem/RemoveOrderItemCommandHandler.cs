using MiniOrderSystem.Application.UseCases.Products;
using MiniOrderSystem.Domain.Common;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.RemoveOrderItem
{
    public class RemoveOrderItemCommandHandler
        (
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IClient client
        ) : IRequestHandler<RemoveOrderItemCommand, Result>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IClient _client = client;

        public async Task<Result> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _customerRepository.GetByTokenAsync(_client.Token!.Value, cancellationToken);
            var foundedActiveOrder = await _orderRepository.GetActivePreInvoiceOrderAsync(currentUser!.Id, cancellationToken);

            if (foundedActiveOrder is null)
                return Result.Failure(OrderMessages.NotFound);

            if (foundedActiveOrder.CustomerId != currentUser.Id)
                return Result.Failure(OrderMessages.AccessDenied);

            var foundedOrderItem = await _orderRepository.GetItemAsync(foundedActiveOrder.Id, request.ProductId, false, cancellationToken);
            if (foundedOrderItem is null)
                return Result.Failure(ProductMessages.NotFound);

            return await _orderRepository.RemoveItemAsync(foundedActiveOrder.Id, request.ProductId, cancellationToken);
        }
    }
}
