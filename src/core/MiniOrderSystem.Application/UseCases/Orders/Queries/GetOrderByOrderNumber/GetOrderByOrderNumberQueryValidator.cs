namespace MiniOrderSystem.Application.UseCases.Orders.Queries.GetOrderByOrderNumber
{
    public class GetOrderByOrderNumberQueryValidator : AbstractValidator<GetOrderByOrderNumberQuery>
    {
        public GetOrderByOrderNumberQueryValidator()
        {
            RuleFor(x => x.OrderNumber)
                .NotNull().WithMessage(OrderMessages.OrderNumberMustBeSent.Message)
                .MaximumLength(10).WithMessage(OrderMessages.InvalidOrderNumber.Message);
        }
    }
}
