using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Domain.Common;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandHandler
        (
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IClient client
        ) : IRequestHandler<ChangeOrderStatusCommand, Result>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IClient _client = client;

        public async Task<Result> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _customerRepository.GetByTokenAsync(_client.Token!.Value, cancellationToken);
            var foundedOrder = await _orderRepository.GetByOrderNumberAsync(request.OrderNumber, cancellationToken);

            if (foundedOrder is null)
                return Result<OrderDto>.Failure(OrderMessages.NotFound);

            if (foundedOrder.CustomerId != currentUser!.Id)
                return Result<OrderDto>.Failure(OrderMessages.AccessDenied);

            return await _orderRepository.ChangeStatusAsync(foundedOrder.Id, request.Status, cancellationToken);
        }
    }
}
