using MiniOrderSystem.Application.Common.Security;
using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Customers.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery : IRequest<Result<CustomerDto>>, IAnonymousRequest
    {
        public required int Id { get; init; }
    }
}
