using MiniOrderSystem.Application.Common;
using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.Helpers;
using MiniOrderSystem.Domain.Common;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler
        (
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IClient client
        ) : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IClient _client = client;

        public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _customerRepository.GetByTokenAsync(_client.Token!.Value, cancellationToken);
            var foundedActiveOrder = await _orderRepository.GetActivePreInvoiceOrderAsync(currentUser!.Id, cancellationToken);

            if (foundedActiveOrder is not null)
                return Result.Success(foundedActiveOrder.ToDto()!);

            var newOrder = OrderHelpers.CreateInstance(currentUser.Id);
            var dbResult = await _orderRepository.AddAsync(newOrder, cancellationToken);

            return dbResult is null
                ? Result.Failure<OrderDto>(CommonMessages.Database.InsertFailed)
                : Result.Success(dbResult.ToDto()!);
        }
    }
}
