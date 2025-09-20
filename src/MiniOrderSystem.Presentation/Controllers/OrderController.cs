using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.UseCases.Orders.Commands.ChangeOrderStatus;
using MiniOrderSystem.Application.UseCases.Orders.Commands.CreateOrder;
using MiniOrderSystem.Application.UseCases.Orders.Commands.RemoveOrderItem;
using MiniOrderSystem.Application.UseCases.Orders.Commands.UpsertOrderItem;
using MiniOrderSystem.Application.UseCases.Orders.Queries.GetOrderByOrderNumber;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Presentation.Controllers
{
    public class OrderController(ISender sender) : BaseController(sender)
    {
        [HttpPost("/api/orders")]
        [ProducesResponseType(typeof(Result<OrderDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command, CancellationToken cancellationToken = default)
            => OK(await MediatR.Send(command, cancellationToken));

        [HttpPost("/api/orders/items")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpsertItem([FromBody] UpsertOrderItemCommand command, CancellationToken cancellationToken = default)
            => OK(await MediatR.Send(command, cancellationToken));

        [HttpDelete("/api/orders/items/{product_id:int}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveItem([FromRoute] int product_id, CancellationToken cancellationToken = default)
            => OK(await MediatR.Send(new RemoveOrderItemCommand { ProductId = product_id }, cancellationToken));

        [HttpGet("/api/orders/{order_number}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderByNumber([FromRoute] string order_number, CancellationToken cancellationToken = default)
            => OK(await MediatR.Send(new GetOrderByOrderNumberQuery { OrderNumber = order_number }, cancellationToken));

        [HttpPut("/api/orders/change-status")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeOrderStatusCommand command, CancellationToken cancellationToken = default)
            => OK(await MediatR.Send(command, cancellationToken));
    }
}
