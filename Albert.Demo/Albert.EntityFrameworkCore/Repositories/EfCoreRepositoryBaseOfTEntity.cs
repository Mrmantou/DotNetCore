using Albert.Domain.Entities;
using Albert.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Albert.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity//, IAggregateRoot
        where TDbContext : DbContext
    {
        public EfCoreRepositoryBase(TDbContext dbContext) : base(dbContext)
        {
        }
    }
}
