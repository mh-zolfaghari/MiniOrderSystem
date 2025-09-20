using MiniOrderSystem.Application.Helpers;
using MiniOrderSystem.Application.UseCases.Products;
using MiniOrderSystem.Domain.Common;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.UpsertOrderItem
{
    public class UpsertOrderItemCommandHandler
        (
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IClient client
        ) : IRequestHandler<UpsertOrderItemCommand, Result>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IClient _client = client;

        public async Task<Result> Handle(UpsertOrderItemCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _customerRepository.GetByTokenAsync(_client.Token!.Value, cancellationToken);
            var foundedActiveOrder = await _orderRepository.GetActivePreInvoiceOrderAsync(currentUser!.Id, cancellationToken);

            if (foundedActiveOrder is not null)
            {
                if (foundedActiveOrder.CustomerId != currentUser.Id)
                    return Result.Failure(OrderMessages.AccessDenied);

                var foundedOrderItem = await _orderRepository.GetItemAsync(foundedActiveOrder.Id, request.ProductId, true, cancellationToken);
                if (foundedOrderItem is not null)
                    return await _orderRepository.UpdateItemAsync(foundedOrderItem.UpdateQuantity(request.Quantity), cancellationToken);
                else
                {
                    var foundedProduct = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
                    if (foundedProduct is null)
                        return Result.Failure(ProductMessages.NotFound);

                    return await _orderRepository.AddItemAsync(request.CreateInstance(foundedActiveOrder.Id, foundedProduct), cancellationToken);
                }
            }
            else
            {
                var newOrder = OrderHelpers.CreateInstance(currentUser.Id);
                foundedActiveOrder = await _orderRepository.AddAsync(newOrder, cancellationToken);

                if (foundedActiveOrder is null)
                    return Result.Failure(OrderMessages.NotFound);

                var foundedProduct = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
                if (foundedProduct is null)
                    return Result.Failure(ProductMessages.NotFound);

                return await _orderRepository.AddItemAsync(request.CreateInstance(foundedActiveOrder.Id, foundedProduct), cancellationToken);
            }
        }
    }
}
