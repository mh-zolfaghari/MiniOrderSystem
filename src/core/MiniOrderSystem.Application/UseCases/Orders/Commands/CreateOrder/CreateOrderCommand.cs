using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand : IRequest<Result<OrderDto>>;
}
