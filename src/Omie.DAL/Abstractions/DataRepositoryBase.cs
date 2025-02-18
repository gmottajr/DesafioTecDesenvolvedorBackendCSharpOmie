using System.Linq.Expressions;
using Omie.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Omie.DAL.Abstractions;

/// <summary>
/// A generic repository for managing data access for entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
public class DataRepositoryBase<TEntity, TKey> : IDataRepositoryBase<TEntity, TKey> 
    where TEntity : EntityBaseRoot<TKey>
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public DataRepositoryBase(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>() ?? throw new InvalidOperationException("DbSet could not be initialized.");
    }

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            // Add logging if required
            throw new InvalidOperationException("An error occurred while retrieving entities.", ex);
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Gets an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity, if found; otherwise, null.</returns>
    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id), "The ID cannot be null.");

        try
        {
            return await _dbSet.FindAsync(id);
        }
        catch (Exception ex)
        {
            // Add logging if required
            throw new InvalidOperationException($"An error occurred while retrieving the entity with ID {id}.", ex);
        }
    }

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public async Task AddAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "The entity cannot be null.");

        try
        {
            await _dbSet.AddAsync(entity);
        }
        catch (Exception ex)
        {
            // Add logging if required
            throw new InvalidOperationException("An error occurred while adding the entity.", ex);
        }
    }

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public void Update(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "The entity cannot be null.");

        try
        {
            _dbSet.Update(entity);
        }
        catch (Exception ex)
        {
            // Add logging if required
            throw new InvalidOperationException("An error occurred while updating the entity.", ex);
        }
    }

    /// <summary>
    /// Deletes an entity from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    public async Task DeleteAsync(TKey id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id), "The ID cannot be null.");

        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new InvalidOperationException($"No entity found with ID {id} to delete.");

            _dbSet.Remove(entity);
        }
        catch (Exception ex)
        {
            // Add logging if required
            throw new InvalidOperationException($"An error occurred while deleting the entity with ID {id}.", ex);
        }
    }

    /// <summary>
    /// Saves all changes made in the context to the database.
    /// </summary>
    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Add logging if required
            throw new InvalidOperationException("An error occurred while saving changes to the database.", ex);
        }
    }
}
