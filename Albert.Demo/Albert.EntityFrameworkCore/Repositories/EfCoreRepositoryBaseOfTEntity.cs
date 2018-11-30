using Albert.Core.Entities;
using Albert.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : DbContext
    {

    }
}
