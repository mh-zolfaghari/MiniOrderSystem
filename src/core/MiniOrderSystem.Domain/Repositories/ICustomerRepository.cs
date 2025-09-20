using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Customer?> GetByTokenAsync(Guid token, CancellationToken cancellationToken);
        Task<IEnumerable<Customer>> GetAllAsync(string? name, string? phoneNumber, CancellationToken cancellationToken);
        Task<Result> AddAsync(Customer customer, CancellationToken cancellationToken);
        Task<bool> IsExistsAsync(string phoneNumebr, CancellationToken cancellationToken);
    }
}
