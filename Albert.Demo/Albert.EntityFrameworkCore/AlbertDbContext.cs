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
            var typesToRegister = configAssembly.GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.BaseType.GetGenericTypeDefinition() == typeof(AlbertEntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
