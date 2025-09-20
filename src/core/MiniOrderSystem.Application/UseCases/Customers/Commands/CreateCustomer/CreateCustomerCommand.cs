using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Customers.Commands.CreateCustomer
{
    public record CreateCustomerCommand : IRequest<Result>
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
}
