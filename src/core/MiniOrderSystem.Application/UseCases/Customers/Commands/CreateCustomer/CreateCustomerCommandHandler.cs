using MiniOrderSystem.Application.Common;
using MiniOrderSystem.Application.Helpers;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Application.UseCases.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler(ICustomerRepository customerRepository) : IRequestHandler<CreateCustomerCommand, Result>
    {
        public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var newCustomer = request.ToModel();
            var dbResult = await customerRepository.AddAsync(newCustomer, cancellationToken);

            return dbResult.IsFailure
                ? Result.Failure(CommonMessages.Database.InsertFailed)
                : Result.Success();
        }
    }
}
