using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.EntityFrameworkCore.Mapping
{
    public abstract class AlbertEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
