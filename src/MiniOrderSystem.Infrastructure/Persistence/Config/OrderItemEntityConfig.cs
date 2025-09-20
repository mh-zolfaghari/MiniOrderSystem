using MiniOrderSystem.Domain.Entities;

namespace MiniOrderSystem.Infrastructure.Persistence.Config
{
    public class OrderItemEntityConfig : BaseEntityConfig<OrderItem>
    {
        public override void ConfigureEntity(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Metadata.SetSchema(Consts.EntitySchema.ORDER);

            builder
                .Property(x => x.OrderId)
                .IsRequired();
            builder
                .HasOne(d => d.Order)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId);

            builder
               .Property(x => x.Quantity)
               .IsRequired();

            builder
                .Property(x => x.Price)
                .HasPrecision(12, 2)
                .IsRequired();

            builder
                .HasIndex(x => new { x.OrderId, x.ProductId })
                .IsUnique();

            builder
                .Property(x => x.RowVersion)
                .IsRowVersion()
                .HasComment("To track changes and control concurrency.");
        }
    }
}
