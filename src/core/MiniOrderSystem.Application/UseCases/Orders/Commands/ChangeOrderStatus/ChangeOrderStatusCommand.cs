using MiniOrderSystem.Domain.Types;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.ChangeOrderStatus
{
    public record ChangeOrderStatusCommand : IRequest<Result>
    {
        public string OrderNumber { get; set; } = string.Empty;
        public OrderStatus Status { get; set; } = OrderStatus.PreInvoice;
    }
}
