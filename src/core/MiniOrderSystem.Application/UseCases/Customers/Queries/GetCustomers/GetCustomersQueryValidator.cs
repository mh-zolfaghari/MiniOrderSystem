using MiniOrderSystem.Shared.Extensions;

namespace MiniOrderSystem.Application.UseCases.Customers.Queries.GetCustomers
{
    public class GetCustomersQueryValidator : AbstractValidator<GetCustomersQuery>
    {
        public GetCustomersQueryValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(150).WithMessage(CustomerMessages.InvalidName.Message);

            RuleFor(x => x.PhoneNumber)
                .Must(PhoneNumberBeValid).WithMessage(CustomerMessages.InvalidPhoneNumberFormat.Message);
        }

        bool PhoneNumberBeValid(string? phoneNumber)
            => string.IsNullOrWhiteSpace(phoneNumber) || ValidationExtensions.IsMobileNo(phoneNumber);
    }
}
