using MiniOrderSystem.Domain.Types;

namespace MiniOrderSystem.Application.UseCases.Orders.Commands.ChangeOrderStatus
{
    public class ChangeOrderStatusCommandValidator : AbstractValidator<ChangeOrderStatusCommand>
    {
        public ChangeOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderNumber)
                .NotNull().WithMessage(OrderMessages.OrderNumberMustBeSent.Message)
                .MaximumLength(10).WithMessage(OrderMessages.InvalidOrderNumber.Message);

            RuleFor(x => x.Status)
                .Must(status => Enum.IsDefined(typeof(OrderStatus), status) && status != OrderStatus.PreInvoice).WithMessage(OrderMessages.InvalidStatus.Message);
        }
    }
}
