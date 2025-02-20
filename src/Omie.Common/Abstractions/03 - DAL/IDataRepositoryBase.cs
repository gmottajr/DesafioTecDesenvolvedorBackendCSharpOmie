using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Omie.Common.Abstractions.DAL.Reposotories;

    /// <summary>
    /// Generic repository abstraction to manage data access for entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IDataRepositoryBase<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>An enumerable of all entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        /// <summary>
        /// Asynchronously retrieves a collection of entities from the database.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to retrieve.</typeparam>
        /// <param name="filter">An optional expression used to filter the entities. If provided, only entities that match the filter will be returned. The expression is applied as a <see cref="Func{TResult}"/> where <typeparamref name="TEntity"/> represents the entity type. If no filter is specified, all entities of the given type will be returned.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is an <see cref="IEnumerable{TEntity}"/> containing the retrieved entities.</returns>
        /// <remarks>
        /// This method allows for the retrieval of entities from the database with an optional filter applied dynamically via a LINQ expression. 
        /// If no filter is provided, all entities of the specified type are returned. The filter parameter uses LINQ's expression tree 
        /// to dynamically create queries based on the provided conditions, enabling flexible and efficient querying.
        /// </remarks>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);

        /// <summary>
        /// Gets an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity, if found; otherwise, null.</returns>
        Task<TEntity?> GetByIdAsync(TKey id);

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an entity from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        Task DeleteAsync(TKey id);

        /// <summary>
        /// Saves all changes made in the context to the database.
        /// </summary>
        Task SaveChangesAsync();
    }
