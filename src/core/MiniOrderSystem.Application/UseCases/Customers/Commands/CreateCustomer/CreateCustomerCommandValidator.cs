using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Extensions;

namespace MiniOrderSystem.Application.UseCases.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(x => x.Name)
                .NotNull().WithMessage(CustomerMessages.NameMustBeSent.Message)
                .MaximumLength(150).WithMessage(CustomerMessages.InvalidName.Message);

            RuleFor(x => x.PhoneNumber)
                .NotNull().WithMessage(CustomerMessages.PhoneNumberMustBeSent.Message)
                .Must(ValidationExtensions.IsMobileNo).WithMessage(CustomerMessages.InvalidPhoneNumberFormat.Message)
                .MustAsync(DuplicatePhoneNumberAsync).WithMessage(CustomerMessages.PhoneNumberIsExist.Message);

            RuleFor(x => x.Country)
                .NotNull().WithMessage(CustomerMessages.CountryMustBeSent.Message)
                .MaximumLength(100).WithMessage(CustomerMessages.InvalidCountry.Message);

            RuleFor(x => x.City)
                .NotNull().WithMessage(CustomerMessages.CityMustBeSent.Message)
                .MaximumLength(100).WithMessage(CustomerMessages.InvalidCity.Message);

            RuleFor(x => x.Street)
                .NotNull().WithMessage(CustomerMessages.StreetMustBeSent.Message)
                .MaximumLength(250).WithMessage(CustomerMessages.InvalidStreet.Message);

            RuleFor(x => x.PostalCode)
                .NotNull().WithMessage(CustomerMessages.PostalCodeMustBeSent.Message)
                .Must(ValidationExtensions.IsPostalCode).WithMessage(CustomerMessages.InvalidPostalCodeFormat.Message);
        }

        private async Task<bool> DuplicatePhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
            => !(await _customerRepository.IsExistsAsync(phoneNumber, cancellationToken));
    }
}
