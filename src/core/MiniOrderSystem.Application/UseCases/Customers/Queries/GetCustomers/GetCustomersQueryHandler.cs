using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.Helpers;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Customers.Queries.GetCustomers
{
    public class GetCustomersQueryHandler(ICustomerRepository customerRepository) : IRequestHandler<GetCustomersQuery, Result<IEnumerable<CustomerDto>>>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<Result<IEnumerable<CustomerDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(request.Name, request.PhoneNumber, cancellationToken);
            return Result.Success<IEnumerable<CustomerDto>>(customers?.Any() != true ? [] : customers.Select(x => x.ToDto()!));
        }
    }
}
