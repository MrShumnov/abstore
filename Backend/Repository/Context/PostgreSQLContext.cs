using System;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.EntityConfiguration;

namespace Repository.Context
{
    public class PostgreSQLContext : BaseContext
    {
        public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options)
        {
            // Database.EnsureCreated();
        }
    }
}
