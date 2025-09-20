using MiniOrderSystem.Domain.Common;
using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Shared;
using MiniOrderSystem.Shared.Result;

namespace MiniOrderSystem.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        #region DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger) : base(options) => _logger = logger;

        #region Overriding Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Disable lazy loading
            optionsBuilder.ConfigureWarnings(warning => warning.Ignore(CoreEventId.DetachedLazyLoadingWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Consts.EntitySchema.BASE);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var entityTypes = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            if (entityTypes?.Any() == true)
            {
                foreach (var relationship in entityTypes)
                    relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }

        public new async Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ChangeTracker.HasChanges())
                    return Result.Success();

                DispatchBaseEntityPropertise();
                await base.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                LoggingException(ex);
                return ConstantMessages.SaveChangesFailed;
            }
        }
        #endregion

        #region Optimization
        private void DispatchBaseEntityPropertise()
        {
            var entries = ChangeTracker.Entries();
            if (entries?.Any() == true)
            {
                foreach (var entry in entries)
                {
                    CheckingAndSetSystematicFields(entry);
                }
            }

        }

        private void CheckingAndSetSystematicFields(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    {
                        SetSystematicModificationFields(entry, DateTime.UtcNow, null);
                        break;
                    }
                case EntityState.Modified:
                    {
                        SetSystematicModificationFields(entry, null, DateTime.UtcNow);
                        break;
                    }
            }

            static void SetSystematicModificationFields(EntityEntry entry, DateTime? createdAt, DateTime? updatedAt)
            {
                if (entry.Entity is ICreationProps && createdAt is not null)
                    entry.Property(nameof(ICreationProps.CreatedAt)).CurrentValue = createdAt;
                if (entry.Entity is IModificationProps)
                    entry.Property(nameof(IModificationProps.UpdatedAt)).CurrentValue = updatedAt;
            }
        }

        private void LoggingException(Exception ex)
        {
            switch (ex)
            {
                case DbUpdateConcurrencyException:
                    _logger?.LogCritical(ex, "DataBase_UpdateConcurrency_Exception");
                    break;
                case DbUpdateException:
                    _logger?.LogCritical(ex, "DataBase_Update_Exception");
                    break;
                case ValidationException:
                    _logger?.LogCritical(ex, "DataBase_Validation_Exception");
                    break;
                case SqlException:
                    _logger?.LogCritical(ex, "DataBase_SQL_Exception");
                    break;
                case ObjectDisposedException:
                    _logger?.LogCritical(ex, "DataBase_ContextObjectDisposed_Exception");
                    break;
                case OperationCanceledException:
                    _logger?.LogCritical(ex, "DataBase_OperationCanceled_Exception");
                    break;
                default:
                    _logger?.LogCritical(ex, "DataBase_Exception");
                    break;
            }
        }
        #endregion
    }
}
