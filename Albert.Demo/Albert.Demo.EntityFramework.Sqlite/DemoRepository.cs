using Albert.Domain.Entities;
using Albert.EntityFrameworkCore.Repositories;

namespace Albert.Demo.EntityFramework.Sqlite
{
    public class DemoRepository<TEntity, TPrimaryKey> : EfCoreRepositoryBase<DemoContext, TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public DemoRepository(DemoContext dbContext) : base(dbContext)
        {
        }
    }

    public class DemoRepository<TEntity> : EfCoreRepositoryBase<DemoContext, TEntity>
        where TEntity : class, IEntity
    {
        public DemoRepository(DemoContext dbContext) : base(dbContext)
        {
        }
    }
}
