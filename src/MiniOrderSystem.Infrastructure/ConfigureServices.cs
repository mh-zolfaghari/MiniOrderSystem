using MiniOrderSystem.Domain.Repositories;
using MiniOrderSystem.Infrastructure.Persistence.Context;
using MiniOrderSystem.Infrastructure.Persistence.Repositories;

namespace MiniOrderSystem.Infrastructure
{
    /// <summary>
    /// This extension is programmed for registering Infrastructure services .
    /// </summary>
    public static class ConfigureServices
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterApplicationDbContextServices(configuration);
            services.RegisterRepositories();

            return services;
        }

        private static IServiceCollection RegisterApplicationDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString(nameof(ApplicationDbContext)), (db) => { db.MigrationsHistoryTable("MigrationHistory"); })
                   .LogTo(Console.WriteLine, LogLevel.Information);
            });
            services.AddScoped<ApplicationDbContextInitializer>();

            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, CustomerRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
