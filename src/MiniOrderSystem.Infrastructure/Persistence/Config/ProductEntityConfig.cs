using MiniOrderSystem.Domain.Entities;
using MiniOrderSystem.Infrastructure.Persistence.Consts;

namespace MiniOrderSystem.Infrastructure.Persistence.Config
{
    public class ProductEntityConfig : BaseEntityConfig<Product>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Product> builder)
        {
            builder.Metadata.SetSchema(Consts.EntitySchema.PRODUCT);

            builder
                .Property(x => x.Name)
                .HasColumnType(SqlTypes.NVARCHAR)
                .HasMaxLength(300)
                .IsRequired();

            builder
                .Property(x => x.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasColumnType(SqlTypes.NVARCHAR)
                .HasMaxLength(3000);
        }
    }
}
