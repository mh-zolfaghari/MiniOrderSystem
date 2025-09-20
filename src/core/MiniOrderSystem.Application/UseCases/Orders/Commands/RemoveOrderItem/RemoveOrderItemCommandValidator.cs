using MiniOrderSystem.Application.UseCases.Products;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.RemoveOrderItem
{
    public class RemoveOrderItemCommandValidator : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage(ProductMessages.InvalidId.Message);
        }
    }
}
