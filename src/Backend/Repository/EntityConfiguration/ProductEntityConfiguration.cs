using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity;

namespace Repository.EntityConfiguration
{
    class ProductEntityConfiguration : BaseEntityConfiguration<ProductEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("products");

            builder.Property(x => x.Symbol).HasColumnName("symbol").IsRequired();
            builder.Property(x => x.Price).HasColumnName("price").IsRequired();
            builder.Property(x => x.Sale).HasColumnName("sale").IsRequired();
            builder.Property(x => x.Description).HasColumnName("description").IsRequired();
        }
    }
}
