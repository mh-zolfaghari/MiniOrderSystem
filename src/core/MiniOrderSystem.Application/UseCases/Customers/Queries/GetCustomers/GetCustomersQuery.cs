using MiniOrderSystem.Application.Common.Security;
using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Customers.Queries.GetCustomers
{
    public record GetCustomersQuery : IRequest<Result<IEnumerable<CustomerDto>>>, IAnonymousRequest
    {
        public string? Name { get; set; } = default;
        public string? PhoneNumber { get; set; } = default;
    }
}
