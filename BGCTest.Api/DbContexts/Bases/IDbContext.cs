using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BGCTest.Api.DbContexts.Bases
{
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// Get provides access to database related information and operations for this context.
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// Creates a <see cref="DbSet{TEntity}"/> that can be used to query and
        /// save instances of <typeparamref name="TEntity"/>.
        /// Entity Framework Core does not support multiple parallel operations being run
        /// on the same <see cref="DbContext"/> instance. This includes both parallel execution of async
        /// queries and any explicit concurrent use from multiple threads. Therefore, always
        /// await async calls immediately, or use separate DbContext instances for operations
        /// that execute in parallel. See Avoiding <see cref="DbContext"/> threading issues for more information.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Gets an <see cref="EntityEntry"/> for the given entity.
        /// The entry provides access to change tracking information and operations for the entity.
        /// This method may be called on an entity that is not tracked. You can then set
        /// the <see cref="EntityEntry.State"/> property on
        /// the returned entry to have the context begin tracking the entity in the specified state.
        /// </summary>
        /// <param name="entity">The entity to get the entry for.</param>
        /// <returns></returns>
        EntityEntry Entry(object entity);

        /// <summary>
        /// Gets an <see cref="EntityEntry{TEntity}"/> for the given
        /// entity. The entry provides access to change tracking information and operations
        /// for the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity to get the entry for.</param>
        /// <returns></returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Get provides access to information and operations for entity instances this context is tracking.
        /// </summary>
        ChangeTracker ChangeTracker { get; }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// This method will automatically call <see cref="ChangeTracker.DetectChanges"/>
        /// to discover any changes to entity instances before saving to the underlying database.
        /// This can be disabled via <see cref="ChangeTracker.AutoDetectChangesEnabled"/>.
        /// Entity Framework Core does not support multiple parallel operations being run
        /// on the same DbContext instance. This includes both parallel execution of async
        /// queries and any explicit concurrent use from multiple threads. Therefore, always
        /// await async calls immediately, or use separate <see cref="DbContext"/> instances for operations
        /// that execute in parallel. See Avoiding <see cref="DbContext"/> threading issues for more information.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// This method will automatically call <see cref="ChangeTracker.DetectChanges"/>
        /// to discover any changes to entity instances before saving to the underlying database.
        /// This can be disabled via <see cref="ChangeTracker.AutoDetectChangesEnabled"/>.
        /// Entity Framework Core does not support multiple parallel operations being run
        /// on the same DbContext instance. This includes both parallel execution of async
        /// queries and any explicit concurrent use from multiple threads. Therefore, always
        /// await async calls immediately, or use separate <see cref="DbContext"/> instances for operations
        /// that execute in parallel. See Avoiding <see cref="DbContext"/> threading issues for more information.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        /// Indicates whether <see cref="ChangeTracker.AcceptAllChanges"/>
        /// is called after the changes have been sent successfully to the database.
        /// </param>
        /// <returns></returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// This method will automatically call <see cref="ChangeTracker.DetectChanges"/>
        /// to discover any changes to entity instances before saving to the underlying database.
        /// This can be disabled via <see cref="ChangeTracker.AutoDetectChangesEnabled"/>.
        /// Entity Framework Core does not support multiple parallel operations being run
        /// on the same DbContext instance. This includes both parallel execution of async
        /// queries and any explicit concurrent use from multiple threads. Therefore, always
        /// await async calls immediately, or use separate <see cref="DbContext"/> instances for operations
        /// that execute in parallel. See Avoiding <see cref="DbContext"/> threading issues for more information.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">
        /// Indicates whether <see cref="ChangeTracker.AcceptAllChanges"/>
        /// is called after the changes have been sent successfully to the database.
        /// </param>
        /// <param name="cancellationToken">
        /// A System.Threading.CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// This method will automatically call <see cref="ChangeTracker.DetectChanges"/>
        /// to discover any changes to entity instances before saving to the underlying database.
        /// This can be disabled via <see cref="ChangeTracker.AutoDetectChangesEnabled"/>.
        /// Entity Framework Core does not support multiple parallel operations being run
        /// on the same DbContext instance. This includes both parallel execution of async
        /// queries and any explicit concurrent use from multiple threads. Therefore, always
        /// await async calls immediately, or use separate <see cref="DbContext"/> instances for operations
        /// that execute in parallel. See Avoiding <see cref="DbContext"/> threading issues for more information.
        /// </summary>
        /// <param name="cancellationToken">
        /// A System.Threading.CancellationToken to observe while waiting for the task to complete.
        /// </param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
