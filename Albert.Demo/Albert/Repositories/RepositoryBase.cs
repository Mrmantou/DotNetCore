using Albert.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Albert.Core.Repositories
{
    public class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        //  public DbSet<TEntity> Table=>

        #region Select/Get/Query

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// </summary>
        /// <returns>IQueryable to be used to select entities from database</returns>
        public IQueryable<TEntity> GetAll() { throw new NotImplementedException(); }

        /// <summary>
        /// Used to get a IQueryable that is used to retrieve entities from entire table.
        /// One or more 
        /// </summary>
        /// <param name="propertySelectors">A list of include expressions.</param>
        /// <returns>IQueryable to be used to select entities from database</returns>
        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors) { throw new NotImplementedException(); }

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <returns>List of all entities</returns>
        public List<TEntity> GetAllList() { throw new NotImplementedException(); }

        /// <summary>
        /// Used to get all entities.
        /// </summary>
        /// <returns>List of all entities</returns>
        public Task<List<TEntity>> GetAllListAsync() { throw new NotImplementedException(); }

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        public Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        /// <summary>
        /// Used to run a query over entire entities.
        /// <see cref="UnitOfWorkAttribute"/> attribute is not always necessary (as opposite to <see cref="GetAll"/>)
        /// if <paramref name="queryMethod"/> finishes IQueryable with ToList, FirstOrDefault etc..
        /// </summary>
        /// <typeparam name="T">Type of return value of this method</typeparam>
        /// <param name="queryMethod">This method is used to query over entities</param>
        /// <returns>Query result</returns>
        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        public TEntity Get(TPrimaryKey id) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets an entity with given primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity</returns>
        public Task<TEntity> GetAsync(TPrimaryKey id) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets exactly one entity with given predicate.
        /// Throws exception if no entity or more than one entity.
        /// </summary>
        /// <param name="predicate">Entity</param>
        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        public TEntity FirstOrDefault(TPrimaryKey id) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        public Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        /// <summary>
        /// Creates an entity with given primary key without database access.
        /// </summary>
        /// <param name="id">Primary key of the entity to load</param>
        /// <returns>Entity</returns>
        public TEntity Load(TPrimaryKey id) { throw new NotImplementedException(); }

        #endregion

        #region Insert

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        public TEntity Insert(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">Inserted entity</param>
        public Task<TEntity> InsertAsync(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Inserts a new entity and gets it's Id.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        public TPrimaryKey InsertAndGetId(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Inserts a new entity and gets it's Id.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        public Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity</param>
        public TEntity InsertOrUpdate(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// </summary>
        /// <param name="entity">Entity</param>
        public Task<TEntity> InsertOrUpdateAsync(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// Also returns Id of the entity.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        public TPrimaryKey InsertOrUpdateAndGetId(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Inserts or updates given entity depending on Id's value.
        /// Also returns Id of the entity.
        /// It may require to save current unit of work
        /// to be able to retrieve id.
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Id of the entity</returns>
        public Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity) { throw new NotImplementedException(); }

        #endregion

        #region Update

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity</param>
        public TEntity Update(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Updates an existing entity. 
        /// </summary>
        /// <param name="entity">Entity</param>
        public Task<TEntity> UpdateAsync(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <param name="updateAction">Action that can be used to change values of the entity</param>
        /// <returns>Updated entity</returns>
        public TEntity Update(TPrimaryKey id, Action<TEntity> updateAction) { throw new NotImplementedException(); }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <param name="updateAction">Action that can be used to change values of the entity</param>
        /// <returns>Updated entity</returns>
        public Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction) { throw new NotImplementedException(); }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        public void Delete(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        public Task DeleteAsync(TEntity entity) { throw new NotImplementedException(); }

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        public void Delete(TPrimaryKey id) { throw new NotImplementedException(); }

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary key of the entity</param>
        public Task DeleteAsync(TPrimaryKey id) { throw new NotImplementedException(); }

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        public void Delete(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        /// <summary>
        /// Deletes many entities by function.
        /// Notice that: All entities fits to given predicate are retrieved and deleted.
        /// This may cause major performance problems if there are too many entities with
        /// given predicate.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        #endregion

        #region Aggregates

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        public int Count() { throw new NotImplementedException(); }

        /// <summary>
        /// Gets count of all entities in this repository.
        /// </summary>
        /// <returns>Count of entities</returns>
        public Task<int> CountAsync() { throw new NotImplementedException(); }

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        public int Count(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>Count of entities</returns>
        public long LongCount() { throw new NotImplementedException(); }

        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>Count of entities</returns>
        public Task<long> LongCountAsync() { throw new NotImplementedException(); }

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        public long LongCount(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        public Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) { throw new NotImplementedException(); }

        #endregion
    }
}
