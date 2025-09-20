using MiniOrderSystem.Application.DTOs;
using MiniOrderSystem.Application.Helpers;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler(ICustomerRepository customerRepository) : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var foundedCustomer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);

            return foundedCustomer is null
                ? Result.Failure<CustomerDto>(CustomerMessages.NotFound)
                : Result.Success(foundedCustomer.ToDto()!);
        }
    }
}
