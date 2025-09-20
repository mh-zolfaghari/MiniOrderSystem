using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.Helpers;
using MiniOrderSystem.Domain.Common;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders.Queries.GetOrderByOrderNumber
{
    public class GetOrderByOrderNumberQueryHandler
        (
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IClient client
        ) : IRequestHandler<GetOrderByOrderNumberQuery, Result<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IClient _client = client;

        public async Task<Result<OrderDto>> Handle(GetOrderByOrderNumberQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _customerRepository.GetByTokenAsync(_client.Token!.Value, cancellationToken);
            var foundedOrder = await _orderRepository.GetByOrderNumberAsync(request.OrderNumber, cancellationToken);

            if (foundedOrder is null)
                return Result<OrderDto>.Failure(OrderMessages.NotFound);

            if (foundedOrder.CustomerId != currentUser!.Id)
                return Result<OrderDto>.Failure(OrderMessages.AccessDenied);

            return Result<OrderDto>.Success(foundedOrder.ToDto()!);
        }
    }
}
