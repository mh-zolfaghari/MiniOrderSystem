using MiniOrderSystem.Application.UseCases.Products;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.UpsertOrderItem
{
    public class UpsertOrderItemCommandValidator : AbstractValidator<UpsertOrderItemCommand>
    {
        public UpsertOrderItemCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage(ProductMessages.InvalidId.Message);

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage(OrderMessages.InvalidQuantity.Message);
        }
    }
}
