using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.RemoveOrderItem
{
    public record RemoveOrderItemCommand : IRequest<Result>
    {
        public int ProductId { get; init; }
    }
}
