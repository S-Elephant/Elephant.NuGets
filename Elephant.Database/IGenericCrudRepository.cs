using System.Linq.Expressions;
using Elephant.Types.Interfaces.ResponseWrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Elephant.Database
{
    /// <summary>
    /// Generic CRUD repository base class that also includes retrieving an entity by id.
    /// </summary>
    public interface IGenericCrudRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get all.
        /// </summary>
        Task<List<TEntity>> All(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieve all records with support for <see cref="QueryTrackingBehavior"/> and includes.
        /// </summary>
        /// <example>await myRepository.All(1, QueryTrackingBehavior.NoTracking, CancellationToken.None, x => x.CustomersCrossOrders, x => x.CustomersCrossAddresses)</example>
        Task<List<TEntity>> All(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get by id.
        /// </summary>
        Task<TEntity?> ById(object id, CancellationToken cancellationToken);

        /// <summary>
        /// Count records in this table.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Amount of records in this table.</returns>
        Task<int> Count(CancellationToken cancellationToken);

        /// <summary>
        /// Count records in this table after filtering.
        /// </summary>
        /// <param name="predicate"><paramref name="predicate"/> filter before counting.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Amount of records in this table that matches the <paramref name="predicate"/> filter</returns>
        Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Delete by id.
        /// </summary>
        /// <returns>True if found and deleted (note: doesn't save).</returns>
        bool Delete(object id);

        /// <summary>
        /// Delete by id and save.
        /// </summary>
        Task<IResponseWrapper<int>> DeleteAndSave(object id, CancellationToken cancellationToken);

        /// <summary>
        /// Returns true if this table contains at least one record.
        /// </summary>
        Task<bool> HasAny(CancellationToken cancellationToken);

        /// <summary>
        /// Returns true if this table contains at least one record after filtering.
        /// </summary>
        /// <param name="predicate"><paramref name="predicate"/> filter before counting.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>True if there's at least one record in this table that matches the <paramref name="predicate"/> filter.</returns>
        Task<bool> HasAny(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Insert.
        /// </summary>
        Task Insert(TEntity obj, CancellationToken cancellationToken);

        /// <summary>
        /// Insert one or more.
        /// </summary>
        Task Insert(ICollection<TEntity> objects, CancellationToken cancellationToken);

        /// <summary>
        /// Insert and save.
        /// </summary>
        Task<IResponseWrapper<int>> InsertAndSave(TEntity obj, CancellationToken cancellationToken);

        /// <summary>
        /// Save.
        /// </summary>
        Task<IResponseWrapper<int>> Save(CancellationToken cancellationToken);

        /// <summary>
        /// Update.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="obj"/> wasn't found.</exception>
        void Update(TEntity obj);

        /// <summary>
        /// Update one or more.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if any <paramref name="objects"/> wasn't found.</exception>
        void Update(ICollection<TEntity> objects);

        /// <summary>
        /// Update entity and save.
        /// Does not check if the <paramref name="obj"/> already exists.
        /// </summary>
        Task<IResponseWrapper<int>> UpdateAndSave(TEntity obj, CancellationToken cancellationToken);

        /// <summary>
		/// Delete all rows from the table, resets the auto-increment and saves.
		/// </summary>
		/// <remarks>Works only on relational databases. Does NOT work on an in-memory database.</remarks>
        Task DeleteAllAndResetAutoIncrement(CancellationToken cancellationToken = default, string schema = "dbo");

        #region Transactions

        /// <inheritdoc cref="IContext.BeginTransaction(CancellationToken)"/>
        Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken);

        /// <inheritdoc cref="IContext.CommitTransactionAndDispose(IDbContextTransaction?, CancellationToken)"/>
        Task CommitTransactionAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken);

        /// <inheritdoc cref="IContext.RollbackTransactionAndDispose(IDbContextTransaction?, CancellationToken)"/>
        Task RollbackTransactionAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken);

        #endregion
    }
}
