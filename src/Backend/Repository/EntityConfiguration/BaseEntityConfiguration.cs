using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entity;

namespace Repository.EntityConfiguration
{
    abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.CreationTime).HasColumnName("created");
            builder.Property(x => x.LastChangeTime).HasColumnName("last_changed");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
        }
    }
}
