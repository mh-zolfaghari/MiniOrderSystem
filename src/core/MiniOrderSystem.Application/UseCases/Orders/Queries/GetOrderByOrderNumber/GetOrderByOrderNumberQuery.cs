using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders.Queries.GetOrderByOrderNumber
{
    public record GetOrderByOrderNumberQuery : IRequest<Result<OrderDto>>
    {
        public required string OrderNumber { get; init; }
    }
}
