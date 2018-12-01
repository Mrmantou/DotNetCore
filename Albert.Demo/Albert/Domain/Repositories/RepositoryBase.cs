using Albert.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Albert.Domain.Repositories
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>,IAggregateRoot<TPrimaryKey>
    {
        #region Select/Get/Query

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        public abstract IQueryable<TEntity> GetAll();

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// One or more 
        /// </summary>
        /// <param name="propertySelectors">A list of include expressions.</param>
        /// <returns>IQueryable to be used to select entities from database</returns>
        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            return GetAll();
        }

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <returns>List of all entities</returns>
        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <returns>List of all entities</returns>
        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        /// <summary>
        /// Used to run a query over entire entities.
        /// <see cref="UnitOfWorkAttribute"/> attribute is not always necessary (as opposite to <see cref="GetAll"/>)
        /// if <paramref name="queryMethod"/> finishes IQueryable with ToList, FirstOrDefault etc..
        /// </summary>
        /// <typeparam name="T">Type of return value of this method</typeparam>
        /// <param name="queryMethod">This method is used to query over entities</param>
        /// <returns>Query result</returns>
        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        public virtual TEntity Get(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);

            if (entity == null)
            {
                throw new Exception($"cannot find entity of {typeof(TEntity)} with id={id}");
            }

            return entity;
        }

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new Exception($"cannot find entity of {typeof(TEntity)} with id={id}");
            }

            return entity;
        }

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        /// <summary>
        /// Creates an entity with given primary key without database access.
        /// </summary>
        /// <param name="id">Primary key of the entity to load</param>
        /// <returns>Entity</returns>
        public virtual TEntity Load(TPrimaryKey id)
        {
            return Get(id);
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        public abstract TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        public virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        /// <summary>
        /// Inserts a new entity and gets it's Id.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        public virtual TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return Insert(entity).Id;
        }

        /// <summary>
        /// Inserts a new entity and gets it's Id.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient() ? Insert(entity) : Update(entity);
        }

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.IsTransient() ? await InsertAsync(entity) : await UpdateAsync(entity);
        }

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// Also returns Id of the entity.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        public virtual TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            return InsertOrUpdate(entity).Id;
        }

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// Also returns Id of the entity.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        public virtual Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdate(entity).Id);
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        public abstract TEntity Update(TEntity entity);

        /// <summary>
        /// Updates an existing entity. 
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <param name="updateAction">Action that can be used to change values of the entity</param>
        /// <returns>Updated entity</returns>
        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            return entity;
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <param name="updateAction">Action that can be used to change values of the entity</param>
        /// <returns>Updated entity</returns>
        public virtual async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await GetAsync(id);
            await updateAction(entity);
            return entity;
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        public abstract void Delete(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        public abstract void Delete(TPrimaryKey id);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        public virtual Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }

        #endregion

        #region Aggregates

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        public virtual int Count()
        {
            return GetAll().Count();
        }

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {

            return GetAll().Where(predicate).Count();
        }

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>Count of entities</returns>
        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>Count of entities</returns>
        public virtual Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }

        #endregion

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
