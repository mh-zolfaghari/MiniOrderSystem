namespace MiniOrderSystem.Application.UseCases.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(CustomerMessages.InvalidId.Message);
        }
    }
}
