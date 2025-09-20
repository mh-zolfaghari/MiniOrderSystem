using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Infrastructure.Persistence.Consts;

namespace MiniOrderSystem.Infrastructure.Persistence.Config
{
    public class OrderEntityConfig : BaseEntityConfig<Order>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Order> builder)
        {
            builder.Metadata.SetSchema(Consts.EntitySchema.ORDER);

            builder
                .Property(x => x.Status)
                .IsRequired();

            builder
                .Property(x => x.OrderNumber)
                .HasColumnType(SqlTypes.NVARCHAR)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .HasOne(d => d.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId);
            builder
                .Property(x => x.CustomerId)
                .IsRequired();

            builder
                .Property(x => x.TotalAmount)
                .HasPrecision(18, 2)
                .IsRequired();
        }
    }
}
