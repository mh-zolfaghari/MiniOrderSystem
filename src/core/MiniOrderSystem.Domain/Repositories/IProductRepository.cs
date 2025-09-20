using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetAllAsync(string? name, CancellationToken cancellationToken);
        Task<Result> AddAsync(Product product, CancellationToken cancellationToken);
    }
}
