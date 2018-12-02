using Albert.Domain.Entities;
using Albert.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Demo.EntityFramework.Sqlite
{
    public class FriendRepository<TEntity, TPrimaryKey> : EfCoreRepositoryBase<FriendContext, TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public FriendRepository(FriendContext dbContext) : base(dbContext)
        {
        }
    }
    
    public class FriendRepository<TEntity> : EfCoreRepositoryBase<FriendContext, TEntity> where TEntity : class, IEntity
    {
        public FriendRepository(FriendContext dbContext) : base(dbContext)
        {
        }
    }
}
