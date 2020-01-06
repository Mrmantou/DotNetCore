using Albert.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Albert.EntityFrameworkCore
{
    public class AlbertDbContext : DbContext
    {
        private Assembly configAssembly;

        public AlbertDbContext(DbContextOptions options, Assembly configAssembly) : base(options)
        {
            this.configAssembly = configAssembly;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(configAssembly);
        }
    }
}
