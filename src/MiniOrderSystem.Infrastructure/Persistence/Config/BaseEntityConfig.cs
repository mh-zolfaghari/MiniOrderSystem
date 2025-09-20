using MiniOrderSystem.Domain.Common;

namespace MiniOrderSystem.Infrastructure.Persistence.Config
{
    public abstract class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IBaseEntity, new()
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            #region Default Configuration
            ConfigureCreationProps(builder);
            ConfigureModificationProps(builder);
            #endregion

            // Custom configuration
            ConfigureEntity(builder);
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

        private void ConfigureCreationProps(EntityTypeBuilder<TEntity> builder)
        {
            if (typeof(ICreationProps).IsAssignableFrom(typeof(TEntity)))
            {
                // Config CreatedAt column
                builder
                    .Property(nameof(ICreationProps.CreatedAt))
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                builder
                    .Property(nameof(ICreationProps.CreatedAt)).Metadata
                    .SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            }
        }

        private void ConfigureModificationProps(EntityTypeBuilder<TEntity> builder)
        {
            if (typeof(IModificationProps).IsAssignableFrom(typeof(TEntity)))
            {
                // Config UpdatedAt column
                builder
                    .Property(nameof(IModificationProps.UpdatedAt)).Metadata
                    .SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            }
        }
    }
}
