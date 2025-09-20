using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Infrastructure.Persistence.Context;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository(ApplicationDbContext dbContext) : ICustomerRepository, IClientRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public Task<Result> AddAsync(Customer customer, CancellationToken cancellationToken)
        {
            _dbContext.Customers.Add(customer);
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => _dbContext.Customers
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public Task<Customer?> GetByTokenAsync(Guid token, CancellationToken cancellationToken)
            => _dbContext.Customers
                .FirstOrDefaultAsync(x => x.Token == token, cancellationToken);

        public async Task<IEnumerable<Customer>> GetAllAsync(string? name, string? phoneNumber, CancellationToken cancellationToken)
        {
            var query = _dbContext.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(phoneNumber))
                query = query.Where(x => x.PhoneNumber == phoneNumber);

            return await query
                .Include(x => x.Address)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task<bool> HasAccessAsync(Guid token, CancellationToken cancellationToken)
            => _dbContext.Customers.AnyAsync(x => x.Token == token && x.IsActive == true, cancellationToken);

        public Task<bool> IsExistsAsync(string phoneNumebr, CancellationToken cancellationToken)
            => _dbContext.Customers.AnyAsync(x => x.PhoneNumber == phoneNumebr, cancellationToken);
    }
}
