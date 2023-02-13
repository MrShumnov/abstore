using System;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.EntityConfiguration;

namespace Repository.Context
{
    public abstract class BaseContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderProductEntity> OrdersProducts { get; set; }

        public BaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductEntityConfiguration());
        }
    }
}
