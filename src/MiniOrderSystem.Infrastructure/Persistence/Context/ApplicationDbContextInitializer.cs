using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Domain.ValueObjects;
using MiniOrderSystem.Infrastructure.Persistence.Consts;

namespace MiniOrderSystem.Infrastructure.Persistence.Context
{
    public class ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
    {
        private readonly ILogger<ApplicationDbContextInitializer> _logger = logger;
        private readonly ApplicationDbContext _context = context;

        public async Task InitializeAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        public async Task SeedDataAsync()
        {
            try
            {
                await SeedProductsAsync();
                await SeedCustomersAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task SeedCustomersAsync()
        {
            var foundedCustomers = await _context.Customers.FirstOrDefaultAsync(x => x.Token == SeedData.Customer.Token);
            if (foundedCustomers is null)
            {
                foundedCustomers = new Customer
                {
                    Token = SeedData.Customer.Token,
                    Name = SeedData.Customer.Name,
                    IsActive = true,
                    PhoneNumber = SeedData.Customer.PhoneNumber,
                    Address = new Address
                    (
                        SeedData.Customer.Address.Street,
                        SeedData.Customer.Address.City,
                        SeedData.Customer.Address.Country,
                        SeedData.Customer.Address.PostalCode
                    )
                };

                await _context.Customers.AddAsync(foundedCustomers);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedProductsAsync()
        {
            var existingProductNames = await _context.Products
                .Select(p => p.Name)
                .ToListAsync();

            var productsToAdd = SeedData.Product.DefaultProducts
                .Where(p => !existingProductNames.Contains(p.Name))
                .ToList();

            if (productsToAdd.Any())
            {
                _context.Products.AddRange(productsToAdd);
                await _context.SaveChangesAsync();
            }
        }
    }
}
