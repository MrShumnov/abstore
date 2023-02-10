using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity;

namespace Repository.EntityConfiguration
{
    class UserEntityConfiguration : BaseEntityConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("users");

            builder.Property(x => x.Username).HasColumnName("username").IsRequired();
            builder.Property(x => x.Login).HasColumnName("login").IsRequired();
            builder.Property(x => x.Password).HasColumnName("password").IsRequired();
            builder.Property(x => x.Role).HasColumnName("role").IsRequired().HasConversion<string>();

            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Login).IsUnique();
        }
    }
}
