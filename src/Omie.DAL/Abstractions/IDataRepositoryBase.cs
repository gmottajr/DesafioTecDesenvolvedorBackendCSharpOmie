namespace Omie.DAL.Abstractions;

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
