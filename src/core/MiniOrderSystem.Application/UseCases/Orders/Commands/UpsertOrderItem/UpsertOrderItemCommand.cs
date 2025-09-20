using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.UpsertOrderItem
{
    public record UpsertOrderItemCommand : IRequest<Result>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
