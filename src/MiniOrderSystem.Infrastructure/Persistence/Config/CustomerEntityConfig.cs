using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Infrastructure.Persistence.Consts;

namespace MiniOrderSystem.Infrastructure.Persistence.Config
{
    public class CustomerEntityConfig : BaseEntityConfig<Customer>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Customer> builder)
        {
            builder.Metadata.SetSchema(Consts.EntitySchema.USER);

            builder
                .Property(x => x.Name)
                .HasColumnType(SqlTypes.NVARCHAR)
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(x => x.PhoneNumber)
                .HasColumnType(SqlTypes.VARCHAR)
                .HasMaxLength(15)
                .IsRequired();
            builder
                .HasIndex(x => x.PhoneNumber)
                .HasFilter($"[{nameof(Customer.PhoneNumber)}] IS NOT NULL");

            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            builder
                .OwnsOne(x => x.Address, address =>
                {
                    address
                        .Property(x => x.Country)
                        .HasColumnType(SqlTypes.NVARCHAR)
                        .HasMaxLength(100)
                        .IsRequired();

                    address
                        .Property(x => x.City)
                        .HasColumnType(SqlTypes.NVARCHAR)
                        .HasMaxLength(100)
                        .IsRequired();

                    address
                        .Property(x => x.Street)
                        .HasColumnType(SqlTypes.NVARCHAR)
                        .HasMaxLength(250)
                        .IsRequired();

                    address
                        .Property(x => x.PostalCode)
                        .HasColumnType(SqlTypes.VARCHAR)
                        .HasMaxLength(10)
                        .IsRequired();
                });
        }
    }
}
