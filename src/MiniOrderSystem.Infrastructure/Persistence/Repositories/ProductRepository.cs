using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Infrastructure.Persistence.Context;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Infrastructure.Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public Task<Result> AddAsync(Product product, CancellationToken cancellationToken)
        {
            _dbContext.Products.Add(product);
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<IEnumerable<Product>> GetAllAsync(string? name, CancellationToken cancellationToken)
        {
            var query = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));

            return await query
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
