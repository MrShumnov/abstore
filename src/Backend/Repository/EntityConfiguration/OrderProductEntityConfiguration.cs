using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity;

namespace Repository.EntityConfiguration
{
    class OrderProductEntityConfiguration : BaseEntityConfiguration<OrderProductEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderProductEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("orders_products");

            builder.Property(x => x.OrderId).HasColumnName("order_id").IsRequired();
            builder.Property(x => x.ProductId).HasColumnName("product_id").IsRequired();
            builder.Property(x => x.Qty).HasColumnName("qty").IsRequired();

            builder.HasIndex(x => new { x.OrderId, x.ProductId}).IsUnique();

            builder.HasOne(x => x.Order)
                .WithMany(y => y.OrdersProducts)
                .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Product)
                .WithMany(y => y.OrdersProducts)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
