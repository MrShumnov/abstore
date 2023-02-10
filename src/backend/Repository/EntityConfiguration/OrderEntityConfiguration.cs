using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity;

namespace Repository.EntityConfiguration
{
    class OrderEntityConfiguration : BaseEntityConfiguration<OrderEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("orders");

            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(x => x.Price).HasColumnName("price").IsRequired();
            builder.Property(x => x.Text).HasColumnName("text").IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(y => y.Orders)
                .HasForeignKey(x => x.UserId);
        }
    }
}
