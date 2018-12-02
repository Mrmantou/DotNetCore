using Albert.Domain.Entities;
using Albert.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity, IAggregateRoot
        where TDbContext : DbContext
    {
        public EfCoreRepositoryBase(TDbContext dbContext) : base(dbContext)
        {
        }
    }
}
